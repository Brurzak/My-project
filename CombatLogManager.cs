using System.Collections.Generic;
using UnityEngine;
using BobsBuddy;
using BobsBuddy.Simulation;

public enum TipoAzione
{
    AttaccoInizio,
    AttaccoFine,
    Danno,
    ScudoDivinoEsploso,
    Morte,
    Evocazione,
    BuffStatistiche,
    TriggerAbilita,
    AggiornaMeccaniche,
    SincronizzaStato
}

public class AzioneCombattimento
{
    public TipoAzione Tipo;

    public bool isSourcePlayer;
    public string sourceCardId;
    public int sourceIdUnico;

    public bool isTargetPlayer;
    public string targetCardId;
    public int targetIdUnico;

    public int Valore1;
    public int Valore2;
    public int Valore3;

    public string DescrizioneLog;

    public bool hasTaunt;
    public bool hasDiv;
    public bool hasReborn;
    public bool hasPoisonous;
    public bool hasVenomous;
    public bool hasWindfury;
    public bool hasStealth;

    public int sourceAtk;
    public int sourceHp;
    public int targetAtk;
    public int targetHp;

    public bool isRebornSummon;
    public bool isLethalDamage;
}

public static class CombatLogManager
{
    private struct StatoMinion
    {
        public int atk;
        public int hp;
        public bool taunt;
        public bool div;
        public bool reborn;
        public bool poisonous;
        public bool venomous;
        public bool windfury;
        public bool stealth;

        public static StatoMinion From(Minion m)
        {
            int atk = m.attack();
            int hp = m.health();

            if (EternalKnightRuntimeTracker.IsEternalKnight(m))
            {
                int baseHpCorrection = (m.baseAttack == (m.golden ? 8 : 4) && m.baseHealth == (m.golden ? 2 : 1)) ? (m.golden ? 2 : 1) : 0;
                hp += baseHpCorrection;
            }

            return new StatoMinion
            {
                atk = atk,
                hp = hp,
                taunt = m.taunt,
                div = m.hasDiv,
                reborn = m.reborn,
                poisonous = m.poisonous,
                venomous = m.venomous,
                windfury = m.windfury,
                stealth = m.stealth
            };
        }
        public bool StesseMeccaniche(StatoMinion other)
        {
            return taunt == other.taunt
                && div == other.div
                && reborn == other.reborn
                && poisonous == other.poisonous
                && venomous == other.venomous
                && windfury == other.windfury
                && stealth == other.stealth;
        }
    }

    public static bool isRecording = false;
    public static int NextIdCounter = 1000;
    public static Queue<AzioneCombattimento> logAzioni = new Queue<AzioneCombattimento>();

    public static HashSet<int> minionGiaEvocati = new HashSet<int>();
    public static Dictionary<int, Vector2Int> minionStatsMemoria = new Dictionary<int, Vector2Int>();
    private static Dictionary<int, StatoMinion> minionStatoMemoria = new Dictionary<int, StatoMinion>();

    public static void ResetLog()
    {
        logAzioni.Clear();
        minionGiaEvocati.Clear();
        minionStatsMemoria.Clear();
        minionStatoMemoria.Clear();
        NextIdCounter = 1000;
        isRecording = true;
    }

    public static void ScanForNewMinions(Simulator sim, bool forceRebornForNewMinions = false)
    {
        if (!isRecording || sim == null) return;

        ScanSide(sim.playerSide, true, forceRebornForNewMinions);
        ScanSide(sim.opponentSide, false, forceRebornForNewMinions);
    }

    private static void ScanSide(List<Minion> side, bool isPlayer, bool forceRebornForNewMinions)
    {
        if (side == null) return;

        for (int i = 0; i < side.Count; i++)
        {
            Minion candidate = side[i];
            if (candidate != null && candidate.game_id > 0 && !minionGiaEvocati.Contains(candidate.game_id))
            {
                AutomatonRuntimeTracker.RegisterSummoned(candidate);
            }
        }

        for (int i = 0; i < side.Count; i++)
        {
            Minion m = side[i];
            if (m == null) continue;

            if (m.game_id <= 0) m.game_id = NextIdCounter++;
            if (!minionGiaEvocati.Contains(m.game_id)) AutomatonRuntimeTracker.RegisterSummoned(m);

            StatoMinion statoCorrente = StatoMinion.From(m);

            if (!minionGiaEvocati.Contains(m.game_id))
            {
                minionGiaEvocati.Add(m.game_id);
                MemorizzaStato(m.game_id, statoCorrente);

                bool isReborn = forceRebornForNewMinions || (statoCorrente.hp == 1 && !statoCorrente.reborn && m.maxHealth > 1);
                RegistraEvocazione(m, isPlayer, i, isReborn);
                continue;
            }

            if (!minionStatoMemoria.TryGetValue(m.game_id, out StatoMinion statoVecchio))
            {
                MemorizzaStato(m.game_id, statoCorrente);
                continue;
            }

            // FIX REBORN/BLANCHY: Se la carta era morta (HP <= 0) e viene trovata di nuovo viva, 
            // significa che Bob's Buddy l'ha riciclata per mantenere i potenziamenti.
            if (statoVecchio.hp <= 0 && statoCorrente.hp > 0)
            {
                m.game_id = NextIdCounter++; // Forziamo un nuovo ID per far capire alla UI che è un nuovo corpo
                statoCorrente = StatoMinion.From(m);
                minionGiaEvocati.Add(m.game_id);
                MemorizzaStato(m.game_id, statoCorrente);

                RegistraEvocazione(m, isPlayer, i, true); // La registriamo correttamente come Reborn
                continue;
            }

            int diffAtk = statoCorrente.atk - statoVecchio.atk;
            int diffHp = statoCorrente.hp - statoVecchio.hp;

            if (diffAtk > 0 || diffHp > 0)
            {
                RegistraBuffConStatoFinale(m, Mathf.Max(0, diffAtk), Mathf.Max(0, diffHp), statoCorrente);
            }
            else if (diffAtk != 0 || diffHp != 0)
            {
                RegistraSincronizzazione(m, statoCorrente);
            }

            if (!statoVecchio.StesseMeccaniche(statoCorrente))
            {
                RegistraAggiornamentoMeccaniche(m, statoCorrente);
            }

            MemorizzaStato(m.game_id, statoCorrente);
        }
    }

    private static void MemorizzaStato(int gameId, StatoMinion stato)
    {
        minionStatoMemoria[gameId] = stato;
        minionStatsMemoria[gameId] = new Vector2Int(stato.atk, stato.hp);
    }

    private static AzioneCombattimento CreaAzioneBase(TipoAzione tipo, Minion source, Minion target = null)
    {
        AzioneCombattimento azione = new AzioneCombattimento { Tipo = tipo };

        if (source != null)
        {
            azione.isSourcePlayer = source.ControlledByPlayer;
            azione.sourceCardId = source.CardID;
            azione.sourceIdUnico = source.game_id;
            azione.sourceAtk = source.attack();
            azione.sourceHp = source.health();
            azione.hasTaunt = source.taunt;
            azione.hasDiv = source.hasDiv;
            azione.hasReborn = source.reborn;
            azione.hasPoisonous = source.poisonous;
            azione.hasVenomous = source.venomous;
            azione.hasWindfury = source.windfury;
            azione.hasStealth = source.stealth;
        }

        if (target != null)
        {
            azione.isTargetPlayer = target.ControlledByPlayer;
            azione.targetCardId = target.CardID;
            azione.targetIdUnico = target.game_id;
            azione.targetAtk = target.attack();
            azione.targetHp = target.health();
        }

        return azione;
    }

    private static void ApplicaStatoSorgente(AzioneCombattimento az, StatoMinion stato)
    {
        az.sourceAtk = stato.atk;
        az.sourceHp = stato.hp;
        az.hasTaunt = stato.taunt;
        az.hasDiv = stato.div;
        az.hasReborn = stato.reborn;
        az.hasPoisonous = stato.poisonous;
        az.hasVenomous = stato.venomous;
        az.hasWindfury = stato.windfury;
        az.hasStealth = stato.stealth;
    }

    public static void RegistraAttaccoInizio(Minion attaccante)
    {
        if (!isRecording || attaccante == null) return;

        StatoMinion stato = StatoMinion.From(attaccante);
        MemorizzaStato(attaccante.game_id, stato);

        var az = CreaAzioneBase(TipoAzione.AttaccoInizio, attaccante);
        ApplicaStatoSorgente(az, stato);
        AggiungiAlLog(az);
    }

    public static void RegistraAttaccoFine(Minion attaccante)
    {
        if (!isRecording || attaccante == null) return;

        StatoMinion stato = StatoMinion.From(attaccante);
        MemorizzaStato(attaccante.game_id, stato);

        var az = CreaAzioneBase(TipoAzione.AttaccoFine, attaccante);
        ApplicaStatoSorgente(az, stato);
        AggiungiAlLog(az);
    }

    public static void RegistraDanno(Minion sorgente, Minion bersaglio, int quantita)
    {
        if (!isRecording || bersaglio == null) return;

        bool lethalByKeyword = sorgente != null && quantita > 0 && (sorgente.poisonous || sorgente.venomous);

        var az = CreaAzioneBase(TipoAzione.Danno, sorgente, bersaglio);
        az.Valore1 = quantita;
        az.isLethalDamage = lethalByKeyword;
        AggiungiAlLog(az);

        AggiornaMemoriaDopoDanno(bersaglio, quantita, lethalByKeyword);
    }

    public static void RegistraScudoDivino(Minion sorgente, Minion bersaglio)
    {
        if (!isRecording || bersaglio == null) return;

        AggiungiAlLog(CreaAzioneBase(TipoAzione.ScudoDivinoEsploso, sorgente, bersaglio));
        AggiornaMemoriaMeccanica(bersaglio.game_id, div: false);
    }

    public static void RegistraEvocazione(Minion evocato, bool isPlayerSide, int insertIndex, bool isReborn)
    {
        if (!isRecording || evocato == null) return;

        StatoMinion stato = StatoMinion.From(evocato);
        var az = CreaAzioneBase(TipoAzione.Evocazione, evocato);
        ApplicaStatoSorgente(az, stato);
        az.isSourcePlayer = isPlayerSide;
        az.Valore1 = stato.atk;
        az.Valore2 = stato.hp;
        az.Valore3 = insertIndex;
        az.hasReborn = !isReborn && stato.reborn;
        az.isRebornSummon = isReborn;

        AggiungiAlLog(az);
    }

    public static void RegistraMorte(Minion morto)
    {
        if (!isRecording || morto == null) return;

        AggiungiAlLog(CreaAzioneBase(TipoAzione.Morte, morto));

        if (minionStatoMemoria.TryGetValue(morto.game_id, out StatoMinion stato))
        {
            stato.hp = 0;
            MemorizzaStato(morto.game_id, stato);
        }
    }

    public static void RegistraBuff(Minion bersaglio, int atk, int hp)
    {
        if (!isRecording || bersaglio == null || (atk == 0 && hp == 0)) return;

        StatoMinion baseStato = StatoMinion.From(bersaglio);
        if (minionStatoMemoria.TryGetValue(bersaglio.game_id, out StatoMinion memoria))
        {
            baseStato = memoria;
        }

        StatoMinion statoFinale = baseStato;
        statoFinale.atk = Mathf.Max(0, baseStato.atk + atk);
        statoFinale.hp = Mathf.Max(0, baseStato.hp + hp);

        RegistraBuffConStatoFinale(bersaglio, atk, hp, statoFinale);
        MemorizzaStato(bersaglio.game_id, statoFinale);
    }

    public static void RegistraTriggerAbilita(Minion minion, string nomeAbilita)
    {
        if (!isRecording || minion == null) return;

        var az = CreaAzioneBase(TipoAzione.TriggerAbilita, minion);
        az.DescrizioneLog = nomeAbilita;
        AggiungiAlLog(az);
    }

    private static void RegistraBuffConStatoFinale(Minion bersaglio, int atk, int hp, StatoMinion statoFinale)
    {
        if (atk == 0 && hp == 0) return;

        var az = CreaAzioneBase(TipoAzione.BuffStatistiche, bersaglio);
        az.Valore1 = atk;
        az.Valore2 = hp;
        ApplicaStatoSorgente(az, statoFinale);
        AggiungiAlLog(az);
    }

    private static void RegistraSincronizzazione(Minion bersaglio, StatoMinion stato)
    {
        var az = CreaAzioneBase(TipoAzione.SincronizzaStato, bersaglio);
        ApplicaStatoSorgente(az, stato);
        AggiungiAlLog(az);
    }

    private static void RegistraAggiornamentoMeccaniche(Minion bersaglio, StatoMinion stato)
    {
        var az = CreaAzioneBase(TipoAzione.AggiornaMeccaniche, bersaglio);
        ApplicaStatoSorgente(az, stato);
        AggiungiAlLog(az);
    }

    private static void AggiornaMemoriaDopoDanno(Minion bersaglio, int quantita, bool lethalByKeyword)
    {
        if (bersaglio == null || quantita <= 0) return;

        StatoMinion stato = StatoMinion.From(bersaglio);
        if (minionStatoMemoria.TryGetValue(bersaglio.game_id, out StatoMinion memoria))
        {
            stato = memoria;
        }

        stato.hp = lethalByKeyword ? 0 : Mathf.Max(0, stato.hp - quantita);
        MemorizzaStato(bersaglio.game_id, stato);
    }

    private static void AggiornaMemoriaMeccanica(int gameId, bool? div = null)
    {
        if (!minionStatoMemoria.TryGetValue(gameId, out StatoMinion stato)) return;

        if (div.HasValue) stato.div = div.Value;
        MemorizzaStato(gameId, stato);
    }

    private static void AggiungiAlLog(AzioneCombattimento azione)
    {
        logAzioni.Enqueue(azione);
    }
}