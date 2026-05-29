using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using BobsBuddy.Simulation;
using BobsBuddy.Factory;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("Database")]
    public List<CardData> databaseCarte;
    public Dictionary<string, CardData> databaseTotale = new Dictionary<string, CardData>();

    [Header("UI Risultati e Simulazione")]
    public GameObject resultPopup;
    public TextMeshProUGUI winPercentText;
    public TextMeshProUGUI tiePercentText;
    public TextMeshProUGUI lossPercentText;

    [Header("UI Bottoni (Assegna da Unity)")]
    public GameObject contenitoreBottoni;
    public Button btnSimula;
    public Button btnPulisci;
    public Button btnReset;
    public Button btnX2;
    public TextMeshProUGUI testoFeedbackX2;
    public GameObject menuImpostazioniPanel;

    public bool isSimulating = false;

    private CanvasGroup cgTestoX2;
    private CanvasGroup cgBtnX2;

    public class StatoSlotSalvato
    {
        public string cardId;
        public bool isGolden;
        public int attack;
        public int health;
        public int baseCustomAttack;
        public int baseCustomHealth;
        public bool taunt;
        public bool divineShield;
        public bool venomous;
        public bool poisonous;
        public bool reborn;
        public bool windfury;
        public bool stealth;
    }

    private StatoSlotSalvato[] savedPlayerBoard = new StatoSlotSalvato[7];
    private StatoSlotSalvato[] savedOpponentBoard = new StatoSlotSalvato[7];
    private bool haSalvatoStato = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        if (resultPopup != null) resultPopup.SetActive(false);
        if (menuImpostazioniPanel != null) menuImpostazioniPanel.SetActive(false);

        if (testoFeedbackX2 != null)
        {
            cgTestoX2 = testoFeedbackX2.gameObject.GetComponent<CanvasGroup>();
            if (cgTestoX2 == null) cgTestoX2 = testoFeedbackX2.gameObject.AddComponent<CanvasGroup>();
            cgTestoX2.alpha = 0f;
            Color baseColor = testoFeedbackX2.color;
            baseColor.a = 1f;
            testoFeedbackX2.color = baseColor;
            testoFeedbackX2.gameObject.SetActive(true);
        }

        if (btnX2 != null)
        {
            cgBtnX2 = btnX2.gameObject.GetComponent<CanvasGroup>();
            if (cgBtnX2 == null) cgBtnX2 = btnX2.gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Start()
    {
        TextAsset fileJson = Resources.Load<TextAsset>("Data/cards");
        if (fileJson == null) return;
        var tutteLeCarte = JsonConvert.DeserializeObject<List<CardData>>(fileJson.text);

        foreach (var c in tutteLeCarte)
        {
            if (!string.IsNullOrEmpty(c.id)) databaseTotale[c.id] = c;
        }

        databaseCarte = tutteLeCarte.Where(c => c.set == "BATTLEGROUNDS" && c.techLevel > 0 && c.isBattlegroundsPoolMinion == true).ToList();
        if (ImageCache.Instance != null) foreach (var carta in databaseCarte) if (carta != null) ImageCache.Instance.PrescaricaImmagine(carta.id);

        RinfrescaVisualX2();
    }

    public void ApriMenuImpostazioni() { if (menuImpostazioniPanel != null) menuImpostazioniPanel.SetActive(true); GestisciBottoni(false, false, false, false); }
    public void ChiudiMenuImpostazioni() { if (menuImpostazioniPanel != null) menuImpostazioniPanel.SetActive(false); GestisciBottoni(true, true, true, true); }
    public void CliccaPulisciTutto() { if (isSimulating) return; haSalvatoStato = false; foreach (var slot in BoardSlot.tuttiGliSlot) if (slot != null) slot.ClearSlot(); }

    public void ToggleX2()
    {
        if (GameSettings.Instance != null)
        {
            GameSettings.Instance.isSpeedX2 = !GameSettings.Instance.isSpeedX2;
            RinfrescaVisualX2();

            if (testoFeedbackX2 != null && cgTestoX2 != null)
            {
                cgTestoX2.DOKill();
                testoFeedbackX2.transform.DOKill();
                testoFeedbackX2.text = GameSettings.Instance.isSpeedX2 ? "X2 Attivato!" : "X2 Disattivato!";
                testoFeedbackX2.color = GameSettings.Instance.isSpeedX2 ? Color.green : Color.red;
                cgTestoX2.alpha = 1f;
                testoFeedbackX2.transform.localScale = Vector3.one * 0.6f;
                testoFeedbackX2.transform.DOScale(1.2f, 0.25f).SetEase(Ease.OutBack);
                cgTestoX2.DOFade(0f, 0.3f).SetDelay(1.0f);
            }
        }
    }

    private void RinfrescaVisualX2() { if (cgBtnX2 != null && GameSettings.Instance != null) cgBtnX2.alpha = GameSettings.Instance.isSpeedX2 ? 1.0f : 0.4f; }

    private void GestisciBottoni(bool simula, bool pulisci, bool reset, bool x2)
    {
        if (btnSimula != null) btnSimula.interactable = simula;
        if (btnPulisci != null) btnPulisci.interactable = pulisci;
        if (btnReset != null) btnReset.interactable = reset;
        if (btnX2 != null) btnX2.interactable = x2;
    }

    public void CliccaReset()
    {
        isSimulating = false;
        GestisciBottoni(true, true, true, true);

        if (resultPopup != null) resultPopup.SetActive(false);
        Time.timeScale = 1.0f;

        foreach (var s in BoardSlot.tuttiGliSlot) { s.transform.SetSiblingIndex(s.originalSiblingIndex); }
        foreach (var s in BoardSlot.tuttiGliSlot) { s.gameObject.SetActive(true); s.RipristinaFisicaBase(); }

        if (!haSalvatoStato) { AggiornaTuttiILayoutInScena(); return; }

        var playerSlots = BoardSlot.tuttiGliSlot.Where(s => s != null && s.isPlayerBoard).OrderBy(s => s.transform.GetSiblingIndex()).ToList();
        var oppSlots = BoardSlot.tuttiGliSlot.Where(s => s != null && !s.isPlayerBoard).OrderBy(s => s.transform.GetSiblingIndex()).ToList();

        for (int i = 0; i < 7; i++)
        {
            if (i < playerSlots.Count && playerSlots[i] != null)
            {
                if (savedPlayerBoard[i] != null) RipristinaSlotDaMemoria(playerSlots[i], savedPlayerBoard[i]);
                else playerSlots[i].ClearSlot();
            }
            if (i < oppSlots.Count && oppSlots[i] != null)
            {
                if (savedOpponentBoard[i] != null) RipristinaSlotDaMemoria(oppSlots[i], savedOpponentBoard[i]);
                else oppSlots[i].ClearSlot();
            }
        }
        haSalvatoStato = false;
        AggiornaTuttiILayoutInScena();
    }

    public async void CliccaPerSimulare()
    {
        if (isSimulating) return;
        isSimulating = true;
        SalvaStatoBoard();

        GestisciBottoni(false, false, false, false);

        BobsBuddy.Simulation.Input statePerStatistiche = CreaInputSimulazioneECoppiaID();
        SimulationRunner runner = new SimulationRunner();
        Output result = await runner.SimulateMultiThreaded(statePerStatistiche, 10000, 1, 1500);
        MostraRisultati(result);

        CombatLogManager.ResetLog();
        BobsBuddy.Simulation.Input statePerLog = CreaInputSimulazioneECoppiaID();
        Simulator simDaRegista = new Simulator();
        simDaRegista.SimulateForInput(statePerLog, 1, 1500);
        CombatLogManager.isRecording = false;

        // --- FIX ANIMAZIONI PRIMA SIMULAZIONE ---
        foreach (var s in BoardSlot.tuttiGliSlot)
        {
            if (s != null)
            {
                if (string.IsNullOrEmpty(s.currentCardId))
                {
                    s.gameObject.SetActive(false);
                }
                else if (s.tokenContainer != null)
                {
                    // Distrugge l'offset fantasma del Prefab prima che parta l'attacco
                    s.tokenContainer.transform.DOKill();
                    s.tokenContainer.transform.localPosition = Vector3.zero;
                }
            }
        }

        // Costringe Unity a calcolare le posizioni mondiali vere prima di animare
        Canvas.ForceUpdateCanvases();
        AggiornaTuttiILayoutInScena();
        Canvas.ForceUpdateCanvases();
        // ----------------------------------------

        GestisciBottoni(false, false, true, true);

        await EseguiRegiaPartita();

        if (isSimulating)
        {
            isSimulating = false;
            GestisciBottoni(true, true, true, true);
        }
    }

    private StatoSlotSalvato CreaMemoriaDaSlot(BoardSlot slot)
    {
        return new StatoSlotSalvato
        {
            cardId = slot.currentCardId,
            isGolden = slot.isGolden,
            attack = slot.currentAttack,
            health = slot.currentHealth,
            baseCustomAttack = slot.baseCustomAttack,
            baseCustomHealth = slot.baseCustomHealth,
            taunt = slot.hasTaunt,
            divineShield = slot.hasDivineShield,
            venomous = slot.hasVenomous,
            poisonous = slot.hasPoisonous,
            reborn = slot.hasReborn,
            windfury = slot.hasWindfury,
            stealth = slot.hasStealth
        };
    }

    private void RipristinaSlotDaMemoria(BoardSlot slot, StatoSlotSalvato mem)
    {
        slot.SetCard(mem.cardId);
        if (mem.isGolden) slot.SetGolden(true);

        slot.currentAttack = mem.attack;
        slot.currentHealth = mem.health;
        slot.baseCustomAttack = mem.baseCustomAttack;
        slot.baseCustomHealth = mem.baseCustomHealth;
        slot.hasTaunt = mem.taunt;
        slot.hasDivineShield = mem.divineShield;
        slot.hasVenomous = mem.venomous;
        slot.hasPoisonous = mem.poisonous;
        slot.hasReborn = mem.reborn;
        slot.hasWindfury = mem.windfury;
        slot.hasStealth = mem.stealth;

        slot.AggiornaTestiStats();
        slot.RinfrescaMeccanicheVisive();
        slot.RipristinaIconeIstante();
    }

    private void SalvaStatoBoard()
    {
        var playerSlots = BoardSlot.tuttiGliSlot.Where(s => s != null && s.isPlayerBoard).OrderBy(s => s.transform.GetSiblingIndex()).ToList();
        var oppSlots = BoardSlot.tuttiGliSlot.Where(s => s != null && !s.isPlayerBoard).OrderBy(s => s.transform.GetSiblingIndex()).ToList();

        for (int i = 0; i < 7; i++)
        {
            savedPlayerBoard[i] = (i < playerSlots.Count && playerSlots[i] != null && !string.IsNullOrEmpty(playerSlots[i].currentCardId)) ? CreaMemoriaDaSlot(playerSlots[i]) : null;
            savedOpponentBoard[i] = (i < oppSlots.Count && oppSlots[i] != null && !string.IsNullOrEmpty(oppSlots[i].currentCardId)) ? CreaMemoriaDaSlot(oppSlots[i]) : null;
        }
        haSalvatoStato = true;
    }

    private BobsBuddy.Simulation.Input CreaInputSimulazioneECoppiaID()
    {
        BobsBuddy.Simulation.Input state = new BobsBuddy.Simulation.Input();
        if (GameSettings.Instance != null)
        {
            state.Player.Tier = GameSettings.Instance.playerTavernTier;
            state.Opponent.Tier = GameSettings.Instance.opponentTavernTier;
            state.Player.BloodGemAtkBuff = Mathf.Max(0, GameSettings.Instance.playerBloodGemAttack - 1);
            state.Player.BloodGemHealthBuff = Mathf.Max(0, GameSettings.Instance.playerBloodGemHealth - 1);
            state.Opponent.BloodGemAtkBuff = Mathf.Max(0, GameSettings.Instance.opponentBloodGemAttack - 1);
            state.Opponent.BloodGemHealthBuff = Mathf.Max(0, GameSettings.Instance.opponentBloodGemHealth - 1);
            state.Player.EternalKnightCounter = GameSettings.Instance.playerEternalKnightDeaths;
            state.Opponent.EternalKnightCounter = GameSettings.Instance.opponentEternalKnightDeaths;
            state.Player.AncestralAutomatonCounter = GameSettings.Instance.playerAutomatonSummons;
            state.Opponent.AncestralAutomatonCounter = GameSettings.Instance.opponentAutomatonSummons;
        }

        Simulator tempSim = new Simulator();
        MinionFactory factory = new MinionFactory(tempSim);
        state.Player.Side = new List<BobsBuddy.Minion>();
        state.Opponent.Side = new List<BobsBuddy.Minion>();

        var playerSlots = BoardSlot.tuttiGliSlot.Where(s => s != null && s.isPlayerBoard && !string.IsNullOrEmpty(s.currentCardId)).OrderBy(s => s.transform.GetSiblingIndex()).ToList();
        var oppSlots = BoardSlot.tuttiGliSlot.Where(s => s != null && !s.isPlayerBoard && !string.IsNullOrEmpty(s.currentCardId)).OrderBy(s => s.transform.GetSiblingIndex()).ToList();

        int idCounter = 1;

        void ConfiguraMinion(BoardSlot slot, bool isPlayerSide, List<BobsBuddy.Minion> sideList)
        {
            string idDaCreare = slot.datiAttualiCarta != null ? slot.datiAttualiCarta.id : slot.currentCardId;
            var min = factory.CreateFromCardId(idDaCreare, isPlayerSide);

            if (min.game_id == 0) min = factory.CreateFromCardId(slot.currentCardId, isPlayerSide);

            min.game_id = idCounter++;
            slot.idUnico = min.game_id;
            min.golden = slot.isGolden;

            int trueBaseAtk = slot.currentAttack;
            int trueBaseHp = slot.currentHealth;

            // FIX: Rimuove i buff temporanei prima di inviarli al motore matematico
            if (slot.datiAttualiCarta != null && !string.IsNullOrEmpty(slot.datiAttualiCarta.name))
            {
                string n = slot.datiAttualiCarta.name.ToUpper().Trim();
                if (n.Contains("ETERNAL KNIGHT"))
                {
                    int deaths = isPlayerSide ? GameSettings.Instance.playerEternalKnightDeaths : GameSettings.Instance.opponentEternalKnightDeaths;
                    trueBaseAtk -= ((slot.isGolden ? 8 : 4) * deaths);
                    trueBaseHp -= ((slot.isGolden ? 4 : 2) * deaths);
                }
                else if (n.Contains("AUTOMATON"))
                {
                    int summons = isPlayerSide ? GameSettings.Instance.playerAutomatonSummons : GameSettings.Instance.opponentAutomatonSummons;
                    trueBaseAtk -= (3 * summons);
                    trueBaseHp -= (2 * summons);
                }
            }

            if (trueBaseAtk != min.baseAttack) min.baseAttack = trueBaseAtk;
            if (trueBaseHp != min.baseHealth) { min.baseHealth = trueBaseHp; min.maxHealth = trueBaseHp; }

            min.taunt = slot.hasTaunt;
            min.poisonous = slot.hasPoisonous;
            min.venomous = slot.hasVenomous;
            min.reborn = slot.hasReborn;
            min.windfury = slot.hasWindfury;
            min.stealth = slot.hasStealth;

            // FIX Scudi divini nativi di Bob's Buddy
            min.div = slot.hasDivineShield ? 1 : 0;

            sideList.Add(min);
            CombatLogManager.minionGiaEvocati.Add(min.game_id);

            if (CombatLogManager.minionStatsMemoria != null)
            {
                CombatLogManager.minionStatsMemoria[min.game_id] = new Vector2Int(min.attack(), min.health());
            }
        }

        foreach (var slot in playerSlots) if (slot != null) ConfiguraMinion(slot, true, state.Player.Side);
        foreach (var slot in oppSlots) if (slot != null) ConfiguraMinion(slot, false, state.Opponent.Side);

        return state;
    }

    private void ApplicaStatoCompleto(BoardSlot slot, AzioneCombattimento azione, bool animaIcone)
    {
        if (slot == null || !slot.gameObject.activeSelf) return;

        bool statsCambiate = slot.currentAttack != azione.sourceAtk || slot.currentHealth != azione.sourceHp;
        slot.currentAttack = azione.sourceAtk;
        slot.currentHealth = azione.sourceHp;

        slot.hasTaunt = azione.hasTaunt;
        slot.hasDivineShield = azione.hasDiv;
        slot.hasReborn = azione.hasReborn;
        slot.hasPoisonous = azione.hasPoisonous;
        slot.hasVenomous = azione.hasVenomous;
        slot.hasWindfury = azione.hasWindfury;
        slot.hasStealth = azione.hasStealth;

        slot.AggiornaTestiStats();
        ApplicaIconeMeccaniche(slot, azione, animaIcone);

        if (statsCambiate && slot.tokenContainer != null) { slot.tokenContainer.transform.DOPunchScale(new Vector3(0.08f, 0.08f, 0), 0.18f, 1); }
    }

    private void ApplicaIconeMeccaniche(BoardSlot slot, AzioneCombattimento azione, bool anima)
    {
        if (slot == null) return;
        ImpostaIcona(slot, slot.tauntIcon, azione.hasTaunt, anima);
        ImpostaIcona(slot, slot.divineShieldIcon, azione.hasDiv, anima);
        ImpostaIcona(slot, slot.rebornIcon, azione.hasReborn && !azione.isRebornSummon, anima);
        ImpostaIcona(slot, slot.poisonousIcon, azione.hasPoisonous, anima);
        ImpostaIcona(slot, slot.venomousIcon, azione.hasVenomous, anima);
    }

    private void ImpostaIcona(BoardSlot slot, GameObject icona, bool attiva, bool anima)
    {
        if (slot == null || icona == null) return;
        Image img = icona.GetComponent<Image>();
        if (attiva)
        {
            if (img != null) { Color c = img.color; c.a = 1f; img.color = c; }
            if (!icona.activeSelf) { slot.PreparaIcona(icona); } else if (anima) { icona.transform.DOKill(); icona.transform.DOPunchScale(Vector3.one * 0.35f, 0.22f, 1); }
        }
        else if (icona.activeSelf)
        {
            icona.transform.DOKill();
            if (anima) { icona.transform.DOScale(Vector3.zero, 0.16f).SetEase(Ease.InBack).OnComplete(() => slot.SpegniIconaSpecifica(icona)); } else { slot.SpegniIconaSpecifica(icona); }
        }
    }

    private BoardSlot TrovaSlotLiberoPerEvocazione(bool isPlayer)
    {
        return BoardSlot.tuttiGliSlot.Where(s => s != null && s.isPlayerBoard == isPlayer && string.IsNullOrEmpty(s.currentCardId)).OrderBy(s => s.transform.GetSiblingIndex()).FirstOrDefault();
    }

    private void PosizionaSlotEvocato(BoardSlot slot, bool isPlayer, int insertIndex)
    {
        if (slot == null) return;

        var slotBoard = BoardSlot.tuttiGliSlot
            .Where(s => s != null && s.isPlayerBoard == isPlayer && s.transform.parent == slot.transform.parent)
            .OrderBy(s => s.transform.GetSiblingIndex())
            .ToList();

        var pieni = slotBoard.Where(s => s != slot && !string.IsNullOrEmpty(s.currentCardId)).ToList();

        if (pieni.Count == 0)
        {
            slot.transform.SetSiblingIndex(0);
            return;
        }

        int targetSibling;

        if (insertIndex >= pieni.Count)
        {
            targetSibling = pieni.Last().transform.GetSiblingIndex() + 1;
        }
        else
        {
            targetSibling = pieni[Mathf.Max(0, insertIndex)].transform.GetSiblingIndex();
            if (slot.transform.GetSiblingIndex() < targetSibling)
            {
                targetSibling--;
            }
        }
        slot.transform.SetSiblingIndex(targetSibling);
    }

    private async Task EseguiRegiaPartita()
    {
        await Task.Delay(1500);
        if (!isSimulating) return;

        Stack<int> stackAttaccanti = new Stack<int>();

        while (CombatLogManager.logAzioni.Count > 0)
        {
            if (!isSimulating) return;

            AzioneCombattimento azione = CombatLogManager.logAzioni.Dequeue();
            BoardSlot slotSorgente = TrovaSlotInBoard(azione.sourceIdUnico);
            BoardSlot slotBersaglio = TrovaSlotInBoard(azione.targetIdUnico);
            float speed = GameSettings.Instance != null && GameSettings.Instance.isSpeedX2 ? 0.6f : 1.2f;

            int currentAttackerId = stackAttaccanti.Count > 0 ? stackAttaccanti.Peek() : -1;

            switch (azione.Tipo)
            {
                case TipoAzione.AttaccoInizio:
                    stackAttaccanti.Push(azione.sourceIdUnico);
                    currentAttackerId = azione.sourceIdUnico;

                    if (slotSorgente != null && slotSorgente.gameObject.activeSelf)
                    {
                        ApplicaStatoCompleto(slotSorgente, azione, false);
                        BoardSlot bersaglioTrovato = null;
                        foreach (var az in CombatLogManager.logAzioni) { if ((az.Tipo == TipoAzione.Danno || az.Tipo == TipoAzione.ScudoDivinoEsploso) && az.sourceIdUnico == azione.sourceIdUnico) { bersaglioTrovato = TrovaSlotInBoard(az.targetIdUnico); break; } }
                        Vector3 mossaTarget = Vector3.zero;
                        if (bersaglioTrovato != null && bersaglioTrovato.gameObject.activeSelf) { Vector3 dif = bersaglioTrovato.transform.position - slotSorgente.transform.position; mossaTarget = slotSorgente.transform.InverseTransformVector(dif) * 0.22f; } else { mossaTarget = new Vector3(0, slotSorgente.isPlayerBoard ? 50f : -50f, 0); }
                        slotSorgente.tokenContainer.transform.DOKill(); slotSorgente.tokenContainer.transform.DOLocalMove(mossaTarget, 0.18f * speed).SetEase(Ease.OutQuad); slotSorgente.tokenContainer.transform.DOScale(slotSorgente.scalaBaseToken * 1.08f, 0.18f * speed);
                    }
                    await Task.Delay((int)(180 * speed));
                    if (!isSimulating) return;
                    break;

                case TipoAzione.BuffStatistiche:
                    if (slotSorgente != null && slotSorgente.gameObject.activeSelf)
                    {
                        slotSorgente.currentAttack = azione.sourceAtk; slotSorgente.currentHealth = azione.sourceHp; slotSorgente.AggiornaTestiStats(); ApplicaIconeMeccaniche(slotSorgente, azione, true);
                        slotSorgente.tokenContainer.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f * speed, 1);
                        if (azione.Valore1 > 0 && slotSorgente.attackText != null) slotSorgente.attackText.transform.DOPunchScale(Vector3.one * 0.8f, 0.3f * speed, 1);
                        if (azione.Valore2 > 0 && slotSorgente.healthText != null) slotSorgente.healthText.transform.DOPunchScale(Vector3.one * 0.8f, 0.3f * speed, 1);
                        await Task.Delay((int)(350 * speed));
                        if (!isSimulating) return;
                    }
                    break;

                case TipoAzione.SincronizzaStato:
                    if (slotSorgente != null && slotSorgente.gameObject.activeSelf) ApplicaStatoCompleto(slotSorgente, azione, false);
                    break;

                case TipoAzione.AggiornaMeccaniche:
                    if (slotSorgente != null && slotSorgente.gameObject.activeSelf) { ApplicaStatoCompleto(slotSorgente, azione, true); await Task.Delay((int)(180 * speed)); if (!isSimulating) return; }
                    break;

                case TipoAzione.Danno:
                case TipoAzione.ScudoDivinoEsploso:
                    List<AzioneCombattimento> danniSimultanei = new List<AzioneCombattimento> { azione };
                    while (CombatLogManager.logAzioni.Count > 0 && (CombatLogManager.logAzioni.Peek().Tipo == TipoAzione.Danno || CombatLogManager.logAzioni.Peek().Tipo == TipoAzione.ScudoDivinoEsploso)) { danniSimultanei.Add(CombatLogManager.logAzioni.Dequeue()); }
                    BoardSlot trueAttacker = TrovaSlotInBoard(currentAttackerId);
                    if (trueAttacker != null && trueAttacker.gameObject.activeSelf)
                    {
                        var hitEvent = danniSimultanei.FirstOrDefault(d => d.sourceIdUnico == currentAttackerId);
                        if (hitEvent != null)
                        {
                            BoardSlot trueTarget = TrovaSlotInBoard(hitEvent.targetIdUnico);
                            if (trueTarget != null && trueTarget.gameObject.activeSelf) { Vector3 dif = trueTarget.transform.position - trueAttacker.transform.position; Vector3 impattoTarget = trueAttacker.transform.InverseTransformVector(dif) * 0.58f; trueAttacker.tokenContainer.transform.DOKill(); trueAttacker.tokenContainer.transform.DOLocalMove(impattoTarget, 0.12f * speed).SetEase(Ease.InQuad); }
                        }
                    }
                    await Task.Delay((int)(120 * speed));
                    if (!isSimulating) return;

                    bool mortiTrovati = false;
                    foreach (var dmgAz in danniSimultanei)
                    {
                        BoardSlot target = TrovaSlotInBoard(dmgAz.targetIdUnico);
                        if (target == null || !target.gameObject.activeSelf) continue;
                        if (dmgAz.Tipo == TipoAzione.Danno)
                        {
                            target.currentHealth = dmgAz.isLethalDamage ? 0 : target.currentHealth - dmgAz.Valore1;
                            target.AggiornaTestiStats(); target.tokenContainer.transform.DOShakePosition(0.3f * speed, new Vector3(25, 0, 0), 30);
                            if (target.minionTokenArt != null) target.minionTokenArt.DOColor(Color.red, 0.15f * speed).SetLoops(2, LoopType.Yoyo);
                        }
                        else
                        {
                            if (target.divineShieldIcon != null && target.divineShieldIcon.activeSelf)
                            {
                                target.divineShieldIcon.transform.DOScale(Vector3.one * 1.8f, 0.2f * speed);
                                target.divineShieldIcon.GetComponent<Image>().DOFade(0, 0.2f * speed).OnComplete(() => { target.divineShieldIcon.SetActive(false); target.divineShieldIcon.transform.localScale = Vector3.one; target.divineShieldIcon.GetComponent<Image>().color = Color.white; });
                            }
                        }
                        if (target.currentHealth <= 0) mortiTrovati = true;
                    }
                    await Task.Delay((int)(450 * speed));
                    if (!isSimulating) return;

                    if (mortiTrovati)
                    {
                        foreach (var dmgAz in danniSimultanei) { BoardSlot target = TrovaSlotInBoard(dmgAz.targetIdUnico); if (target != null && target.gameObject.activeSelf && target.currentHealth <= 0) { target.tokenContainer.transform.DOKill(); target.tokenContainer.transform.DOScale(Vector3.zero, 0.2f * speed); } }
                        await Task.Delay((int)(200 * speed));
                        if (!isSimulating) return;
                        foreach (var dmgAz in danniSimultanei) { BoardSlot target = TrovaSlotInBoard(dmgAz.targetIdUnico); if (target != null && target.gameObject.activeSelf && target.currentHealth <= 0) { target.ClearSlot(); target.gameObject.SetActive(false); } }
                        AggiornaTuttiILayoutInScena(); await Task.Delay((int)(300 * speed));
                        if (!isSimulating) return;
                    }
                    break;

                case TipoAzione.AttaccoFine:
                    if (slotSorgente != null && slotSorgente.gameObject.activeSelf)
                    {
                        ApplicaStatoCompleto(slotSorgente, azione, false);
                        slotSorgente.tokenContainer.transform.DOKill(); slotSorgente.tokenContainer.transform.DOLocalMove(Vector3.zero, 0.3f * speed).SetEase(Ease.OutSine); slotSorgente.tokenContainer.transform.DOScale(slotSorgente.scalaBaseToken, 0.3f * speed);
                    }

                    if (stackAttaccanti.Count > 0 && stackAttaccanti.Peek() == azione.sourceIdUnico)
                    {
                        stackAttaccanti.Pop();
                    }

                    await Task.Delay((int)(300 * speed));
                    if (!isSimulating) return;
                    break;

                // FIX BLANCHY / REBORN ESATTO
                case TipoAzione.Evocazione:
                    var slotVuoto = TrovaSlotLiberoPerEvocazione(azione.isSourcePlayer);
                    if (slotVuoto != null)
                    {
                        slotVuoto.gameObject.SetActive(true);
                        PosizionaSlotEvocato(slotVuoto, azione.isSourcePlayer, azione.Valore3);
                        slotVuoto.SetCard(azione.sourceCardId);
                        slotVuoto.idUnico = azione.sourceIdUnico;

                        // Non c'è più la forzatura a 1 HP. Si fida ciecamente dei dati LIVE di Bob's Buddy!
                        slotVuoto.currentAttack = azione.Valore1;
                        slotVuoto.currentHealth = azione.Valore2;

                        slotVuoto.hasTaunt = azione.hasTaunt;
                        slotVuoto.hasDivineShield = azione.hasDiv;
                        slotVuoto.hasPoisonous = azione.hasPoisonous;
                        slotVuoto.hasVenomous = azione.hasVenomous;
                        slotVuoto.hasWindfury = azione.hasWindfury;
                        slotVuoto.hasStealth = azione.hasStealth;

                        if (azione.isRebornSummon)
                        {
                            slotVuoto.hasReborn = false;
                        }
                        else
                        {
                            slotVuoto.hasReborn = azione.hasReborn;
                        }

                        slotVuoto.originalTokenAttack = slotVuoto.currentAttack;
                        slotVuoto.originalTokenHealth = slotVuoto.currentHealth;
                        slotVuoto.baseCustomAttack = slotVuoto.currentAttack;
                        slotVuoto.baseCustomHealth = slotVuoto.currentHealth;

                        slotVuoto.AggiornaTestiStats();
                        ApplicaIconeMeccaniche(slotVuoto, azione, false);
                        slotVuoto.tokenContainer.transform.DOKill();

                        if (azione.isRebornSummon)
                        {
                            slotVuoto.minionTokenArt.color = new Color(0.6f, 0.8f, 1f, 0f);
                            slotVuoto.minionTokenArt.DOColor(Color.white, 0.6f * speed);
                            slotVuoto.tokenContainer.transform.localScale = Vector3.one * slotVuoto.scalaBaseToken;
                            slotVuoto.tokenContainer.transform.DOPunchScale(new Vector3(0.1f, 0.2f, 0), 0.5f * speed);
                        }
                        else
                        {
                            slotVuoto.tokenContainer.transform.localScale = Vector3.zero;
                            slotVuoto.tokenContainer.transform.DOScale(Vector3.one * slotVuoto.scalaBaseToken, 0.4f * speed).SetEase(Ease.OutBack);
                        }

                        AggiornaTuttiILayoutInScena();
                        await Task.Delay((int)(550 * speed));
                        if (!isSimulating) return;
                    }
                    break;

                case TipoAzione.Morte:
                    if (slotSorgente != null && slotSorgente.gameObject.activeSelf)
                    {
                        slotSorgente.tokenContainer.transform.DOKill(); slotSorgente.tokenContainer.transform.DOScale(Vector3.zero, 0.3f * speed); await Task.Delay((int)(200 * speed));
                        if (!isSimulating) return;
                        slotSorgente.ClearSlot(); slotSorgente.gameObject.SetActive(false); AggiornaTuttiILayoutInScena(); await Task.Delay((int)(300 * speed));
                        if (!isSimulating) return;
                    }
                    break;

                case TipoAzione.TriggerAbilita:
                    if (slotSorgente != null && slotSorgente.gameObject.activeSelf)
                    {
                        slotSorgente.AnimaIconaTrigger(azione.DescrizioneLog, speed);

                        if (currentAttackerId != slotSorgente.idUnico)
                        {
                            slotSorgente.tokenContainer.transform.DOKill();
                            slotSorgente.tokenContainer.transform.DOPunchPosition(new Vector3(0, slotSorgente.isPlayerBoard ? 30 : -30, 0), 0.4f * speed);
                        }

                        await Task.Delay((int)(400 * speed));
                        if (!isSimulating) return;
                    }
                    break;
            }
        }
    }

    public BoardSlot TrovaSlotInBoard(int idBobsBuddy) { return BoardSlot.tuttiGliSlot.FirstOrDefault(s => s != null && s.idUnico == idBobsBuddy && !string.IsNullOrEmpty(s.currentCardId)); }

    private void AggiornaTuttiILayoutInScena()
    {
        CustomBoardLayout[] layouts = FindObjectsByType<CustomBoardLayout>(FindObjectsSortMode.None);
        foreach (var layout in layouts) if (layout != null) layout.AllineaBoardCentrata();
        Canvas.ForceUpdateCanvases();
    }

    private void MostraRisultati(Output result)
    {
        if (resultPopup != null && result != null)
        {
            resultPopup.SetActive(true); resultPopup.transform.DOKill(); resultPopup.transform.localScale = Vector3.zero; resultPopup.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
            winPercentText.text = $"{(result.winRate * 100f):F1}%"; tiePercentText.text = $"{(result.tieRate * 100f):F1}%"; lossPercentText.text = $"{(result.lossRate * 100f):F1}%";
        }
    }
}