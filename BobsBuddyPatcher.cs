using UnityEngine;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BobsBuddy;
using BobsBuddy.Simulation;
using BobsBuddy.Factory;

public class BobsBuddyPatcher : MonoBehaviour
{
    private static bool patchApplicate = false;

    void Awake()
    {
        if (patchApplicate) return;
        patchApplicate = true;

        var harmony = new Harmony("com.bobsbuddy.sniperpatch");
        Debug.Log("[Harmony] Avvio patch Bob's Buddy...");

        ApplicaPatch(harmony, typeof(Simulator), "PerformAttack", new Type[] { typeof(Minion), typeof(Minion) }, typeof(PerformAttack_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "ProcessDamage", new Type[] { typeof(IEnumerable<Damage>) }, typeof(ProcessDamage_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "TrySummonMinion", new Type[] { typeof(Summon), typeof(List<Minion>), typeof(int), typeof(BobsBuddy.Simulation.Entity), typeof(bool) }, typeof(SummonMinion_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "Destroy", new Type[] { typeof(Minion), typeof(BobsBuddy.Simulation.Entity) }, typeof(Destroy_Patch));
        ApplicaPatch(harmony, typeof(Minion), "IncreaseStats", new Type[] { typeof(int), typeof(BobsBuddy.Simulation.Entity) }, typeof(IncreaseStatsSingle_Patch));
        ApplicaPatch(harmony, typeof(Minion), "IncreaseStats", new Type[] { typeof(int), typeof(int), typeof(BobsBuddy.Simulation.Entity) }, typeof(IncreaseStats_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "TriggerDeathrattles", new Type[] { typeof(Minion) }, typeof(TriggerDeathrattles_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "TriggerRally", new Type[] { typeof(Minion) }, typeof(TriggerRally_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "InvokeBattlecry", new Type[] { typeof(Minion) }, typeof(InvokeBattlecry_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "SetupFight", new Type[] { typeof(BobsBuddy.Simulation.Input) }, typeof(SetupFight_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "ResolveDeaths", Type.EmptyTypes, typeof(ResolveDeaths_Patch));
        ApplicaPatch(harmony, typeof(Simulator), "ResolveReborn", Type.EmptyTypes, typeof(ResolveReborn_Patch));
        ApplicaPatch(harmony, typeof(Minion), "OnReborn", Type.EmptyTypes, typeof(MinionOnReborn_Patch));
        ApplicaPatch(harmony, typeof(Minion), "attackBonus", Type.EmptyTypes, typeof(AttackBonus_Patch));
        ApplicaPatch(harmony, typeof(Minion), "healthBonus", Type.EmptyTypes, typeof(HealthBonus_Patch));

        // Controlla eventuali token appena prima della risoluzione dei trigger.
        ApplicaPatch(harmony, typeof(Simulator), "ResolveTriggers", new Type[] { typeof(List<Trigger>) }, typeof(ResolveTriggers_Patch));

        Debug.Log("[Harmony] Patch Bob's Buddy completate.");
    }

    private void ApplicaPatch(Harmony harmony, Type classeOriginale, string nomeMetodo, Type[] parametri, Type classePatch)
    {
        try
        {
            MethodInfo metodoOriginale = AccessTools.Method(classeOriginale, nomeMetodo, parametri);
            if (metodoOriginale == null)
            {
                string firma = string.Join(", ", parametri.Select(p => p.Name));
                Debug.LogWarning($"[Harmony] Metodo non trovato: {classeOriginale.FullName}.{nomeMetodo}({firma})");
                return;
            }

            MethodInfo prefix = AccessTools.Method(classePatch, "Prefix");
            MethodInfo postfix = AccessTools.Method(classePatch, "Postfix");
            harmony.Patch(metodoOriginale, prefix != null ? new HarmonyMethod(prefix) : null, postfix != null ? new HarmonyMethod(postfix) : null);
            Debug.Log($"[Harmony] Patch applicata: {classeOriginale.Name}.{nomeMetodo} -> {classePatch.Name}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Harmony] Errore patch {classeOriginale.FullName}.{nomeMetodo}: {ex}");
        }
    }
}

public struct BonusKeywordState
{
    public bool taunt;
    public bool div;
    public bool poisonous;
    public bool venomous;
    public bool windfury;
    public bool stealth;
}

public static class RebornBonusKeywordTracker
{
    private static readonly Dictionary<Minion, BonusKeywordState> startingKeywords = new Dictionary<Minion, BonusKeywordState>();

    public static void Snapshot(Simulator sim)
    {
        if (sim == null) return;
        SnapshotSide(sim.playerSide);
        SnapshotSide(sim.opponentSide);
    }

    private static void SnapshotSide(List<Minion> side)
    {
        if (side == null) return;

        foreach (Minion minion in side)
        {
            if (minion == null || startingKeywords.ContainsKey(minion)) continue;

            startingKeywords[minion] = new BonusKeywordState
            {
                taunt = minion.taunt,
                div = minion.hasDiv,
                poisonous = minion.poisonous,
                venomous = minion.venomous,
                windfury = minion.windfury,
                stealth = minion.stealth
            };
        }
    }

    public static void ApplyBlanchyKeywords(Minion source, Minion rebornCopy)
    {
        if (source == null || rebornCopy == null) return;

        startingKeywords.TryGetValue(source, out BonusKeywordState start);

        rebornCopy.taunt = source.taunt || start.taunt;
        rebornCopy.div = (source.hasDiv || start.div) ? Math.Max(1, rebornCopy.div) : 0;
        rebornCopy.poisonous = source.poisonous || start.poisonous;
        rebornCopy.venomous = source.venomous || start.venomous;
        rebornCopy.windfury = source.windfury || start.windfury;
        rebornCopy.stealth = source.stealth || start.stealth;
        rebornCopy.reborn = false;
    }
}
public static class AutomatonRuntimeTracker
{
    private static readonly Dictionary<Simulator, HashSet<Minion>> summonedBySimulator = new Dictionary<Simulator, HashSet<Minion>>();
    private static readonly Dictionary<Minion, int> extraOtherSummonsByMinion = new Dictionary<Minion, int>();

    public static void Reset(Simulator sim)
    {
        if (sim == null) return;
        if (summonedBySimulator.TryGetValue(sim, out HashSet<Minion> oldSummoned))
        {
            foreach (Minion oldMinion in oldSummoned)
            {
                extraOtherSummonsByMinion.Remove(oldMinion);
            }
        }
        summonedBySimulator[sim] = new HashSet<Minion>();
    }

    public static void RegisterSummoned(Minion minion, bool isReborn = false)
    {
        if (!IsAutomaton(minion)) return;

        if (!summonedBySimulator.TryGetValue(minion.Simulator, out HashSet<Minion> summoned))
        {
            summoned = new HashSet<Minion>();
            summonedBySimulator[minion.Simulator] = summoned;
        }

        summoned.Add(minion);
        if (isReborn) extraOtherSummonsByMinion[minion] = 1;
    }

    public static int OtherSummonsFor(Minion minion)
    {
        if (minion == null) return 0;

        int menuSummons = 0;
        if (GameSettings.Instance != null)
        {
            menuSummons = minion.ControlledByPlayer ? GameSettings.Instance.playerAutomatonSummons : GameSettings.Instance.opponentAutomatonSummons;
        }

        if (!summonedBySimulator.TryGetValue(minion.Simulator, out HashSet<Minion> summoned)) return menuSummons;

        int combatSummons = 0;
        foreach (Minion summonedMinion in summoned)
        {
            if (summonedMinion != null && summonedMinion.ControlledByPlayer == minion.ControlledByPlayer)
            {
                combatSummons++;
            }
        }

        if (summoned.Contains(minion)) combatSummons--;
        extraOtherSummonsByMinion.TryGetValue(minion, out int extraOtherSummons);
        return Math.Max(0, menuSummons + combatSummons + extraOtherSummons);
    }

    public static bool IsAutomaton(Minion minion)
    {
        return minion != null && (minion.CardID == "BG_TTN_401" || minion.CardID == "BG_TTN_401_G");
    }
}
public static class EternalKnightRuntimeTracker
{
    private class Counts
    {
        public HashSet<Minion> playerDead = new HashSet<Minion>();
        public HashSet<Minion> opponentDead = new HashSet<Minion>();
    }

    private static readonly Dictionary<Simulator, Counts> countsBySimulator = new Dictionary<Simulator, Counts>();

    public static void Reset(Simulator sim)
    {
        if (sim == null) return;
        countsBySimulator[sim] = new Counts();
    }

    public static void ScanDeaths(Simulator sim)
    {
        if (sim == null) return;
        if (!countsBySimulator.TryGetValue(sim, out Counts counts))
        {
            counts = new Counts();
            countsBySimulator[sim] = counts;
        }

        ScanSide(sim.playerSide, counts.playerDead);
        ScanSide(sim.opponentSide, counts.opponentDead);
    }

    private static void ScanSide(List<Minion> side, HashSet<Minion> deadSet)
    {
        if (side == null) return;

        foreach (Minion minion in side)
        {
            if (minion != null && IsEternalKnight(minion) && minion.IsDead())
            {
                deadSet.Add(minion);
            }
        }
    }

    public static int DeathsFor(Minion minion)
    {
        if (minion == null) return 0;

        int menuDeaths = 0;
        if (GameSettings.Instance != null)
        {
            menuDeaths = minion.ControlledByPlayer ? GameSettings.Instance.playerEternalKnightDeaths : GameSettings.Instance.opponentEternalKnightDeaths;
        }

        if (!countsBySimulator.TryGetValue(minion.Simulator, out Counts counts)) return menuDeaths;

        int combatDeaths = minion.ControlledByPlayer ? counts.playerDead.Count : counts.opponentDead.Count;
        return menuDeaths + combatDeaths;
    }

    public static bool IsEternalKnight(Minion minion)
    {
        return minion != null && (minion.CardID == "BG25_008" || minion.CardID == "BG25_008_G");
    }
}

public class SetupFight_Patch
{
    static void Prefix(Simulator __instance)
    {
        EternalKnightRuntimeTracker.Reset(__instance);
        AutomatonRuntimeTracker.Reset(__instance);
    }

    static void Postfix(Simulator __instance)
    {
        RebornBonusKeywordTracker.Snapshot(__instance);
    }
}
public class PerformAttack_Patch
{
    static void Prefix(Simulator __instance, Minion attacker)
    {
        CombatLogManager.ScanForNewMinions(__instance);
        CombatLogManager.RegistraAttaccoInizio(attacker);

        // FIX: SHIP JUMPER (Usa CardID, ControlledByPlayer e playerSide per compatibilità perfetta)
        if (attacker != null && !string.IsNullOrEmpty(attacker.CardID) && (attacker.CardID == "BG35_700" || attacker.CardID == "BG35_700_G"))
        {
            CombatLogManager.RegistraTriggerAbilita(attacker, "Rally");

            // LA CORREZIONE È QUI: Creiamo l'istanza della factory passandole il simulatore corrente
            MinionFactory factory = new MinionFactory(__instance);
            Minion skyPirate = factory.CreateFromCardId("BGS_061t", attacker.ControlledByPlayer);

            if (skyPirate != null)
            {
                skyPirate.baseAttack = attacker.attack();
                skyPirate.baseHealth = attacker.golden ? 2 : 1;

                var targetSide = attacker.ControlledByPlayer ? __instance.playerSide : __instance.opponentSide;
                int spawnIndex = targetSide.IndexOf(attacker) + 1; // Calcolo sicuro dell'indice

                __instance.TrySummonMinion(new Summon(skyPirate), targetSide, spawnIndex, attacker, false);
            }
        }
    }

    static void Postfix(Minion attacker)
    {
        CombatLogManager.RegistraAttaccoFine(attacker);
    }
}

public class ProcessDamage_Patch
{
    static void Prefix(Simulator __instance, IEnumerable<Damage> damageGroup)
    {
        CombatLogManager.ScanForNewMinions(__instance);
        if (!CombatLogManager.isRecording || damageGroup == null) return;

        foreach (var dmg in damageGroup)
        {
            if (dmg.Target == null) continue;

            Minion sorgente = dmg.Source as Minion;
            if (dmg.Target.hasDiv)
            {
                CombatLogManager.RegistraScudoDivino(sorgente, dmg.Target);
            }
            else
            {
                CombatLogManager.RegistraDanno(sorgente, dmg.Target, Mathf.Max(0, dmg.Amount));
            }
        }
    }

    static void Postfix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance);
    }
}

public class ResolveTriggers_Patch
{
    static void Prefix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance);
    }

    static void Postfix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance);
    }
}

public class SummonMinion_Patch
{
    private static void CorreggiEternalKnight(Minion m)
    {
        if (m == null || (m.CardID != "BG25_008" && m.CardID != "BG25_008_G")) return;

        int correctBaseAtk = m.golden ? 8 : 4;
        int wrongBaseHp = m.golden ? 2 : 1;
        int correctBaseHp = m.golden ? 4 : 2;

        if (m.baseAttack == correctBaseAtk && m.baseHealth == wrongBaseHp)
        {
            m.baseHealth = correctBaseHp;
            m.vanillaHealth = correctBaseHp;
            m.maxHealth = correctBaseHp;
        }
    }

    static void Prefix(Summon summon)
    {
        CorreggiEternalKnight(summon?.Minion);
    }

    static void Postfix(Simulator __instance, Summon summon, List<Minion> side, int insertIndex, BobsBuddy.Simulation.Entity source, bool isReborn, List<Minion> __result)
    {
        if (__result != null)
        {
            foreach (var minion in __result)
            {
                CorreggiEternalKnight(minion);
                AutomatonRuntimeTracker.RegisterSummoned(minion, isReborn);
            }
        }

        CombatLogManager.ScanForNewMinions(__instance, isReborn);
    }
}

public class Destroy_Patch
{
    static void Prefix(Minion target)
    {
        CombatLogManager.RegistraMorte(target);
    }
}

public class IncreaseStatsSingle_Patch
{
    static void Prefix(Minion __instance, int by, BobsBuddy.Simulation.Entity source)
    {
        CombatLogManager.RegistraBuff(__instance, by, by);
    }
}

public class IncreaseStats_Patch
{
    static void Prefix(Minion __instance, int attackBuff, int healthBuff, BobsBuddy.Simulation.Entity source)
    {
        CombatLogManager.RegistraBuff(__instance, attackBuff, healthBuff);
    }
}

public class TriggerDeathrattles_Patch
{
    static bool Prefix(Minion minion)
    {
        // FIX: Blocca l'esecuzione errata di Bob's Buddy (Rantolo invece di Rally)
        if (minion != null && !string.IsNullOrEmpty(minion.CardID) && (minion.CardID == "BG35_700" || minion.CardID == "BG35_700_G"))
        {
            return false; // Salta il rantolo fasullo senza registrarlo
        }

        CombatLogManager.RegistraTriggerAbilita(minion, "Rantolo");
        return true;
    }

    static void Postfix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance);
    }
}

public class TriggerRally_Patch
{
    static void Prefix(Minion minion)
    {
        CombatLogManager.RegistraTriggerAbilita(minion, "Rally");
    }

    static void Postfix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance);
    }
}

public class InvokeBattlecry_Patch
{
    static void Prefix(Minion target)
    {
        CombatLogManager.RegistraTriggerAbilita(target, "Grido");
    }

    static void Postfix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance);
    }
}

public class ResolveDeaths_Patch
{
    static void Prefix(Simulator __instance)
    {
        EternalKnightRuntimeTracker.ScanDeaths(__instance);
    }

    static void Postfix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance);
    }
}

public class ResolveReborn_Patch
{
    static void Postfix(Simulator __instance)
    {
        CombatLogManager.ScanForNewMinions(__instance, true);
    }
}

public class MinionOnReborn_Patch
{
    static void Postfix(Minion __instance, ref Func<List<Minion>> __result)
    {
        // Se il minion è nullo o non ha Reborn, lasciamo scorrere
        if (__instance == null || !__instance.reborn) return;

        // 1. LOGICA BLANCHY (già presente e corretta)
        if (__instance.CardID == "BG24_005" || __instance.CardID == "BG24_005_G")
        {
            __result = () =>
            {
                Minion rebornCopy = __instance.Clone();
                RebornBonusKeywordTracker.ApplyBlanchyKeywords(__instance, rebornCopy);
                rebornCopy.baseAttack = __instance.maxAttack;
                rebornCopy.maxAttack = __instance.maxAttack;
                rebornCopy.baseHealth = __instance.maxHealth;
                rebornCopy.maxHealth = __instance.maxHealth;
                return __instance.TrySummonMinion((Summon)rebornCopy, true);
            };
            return;
        }

        // 2. LOGICA AUTOMATON E CAVALIERI ETERNI
        if (AutomatonRuntimeTracker.IsAutomaton(__instance) || EternalKnightRuntimeTracker.IsEternalKnight(__instance))
        {
            __result = () =>
            {
                // Creiamo il clone base pulito (senza buff temporanei o danni)
                Minion minion = __instance.CloneAsVanilla();
                minion.reborn = false;

                // IL FIX: Ignoriamo il blocco a 1 HP imposto nativamente da Bob's Buddy.
                // Assegniamo la salute base pura della carta (che è 1 o 2 per Automaton, 2 o 4 per il Cavaliere).
                // Il sistema delle aure farà poi il suo lavoro matematico su questo valore pulito.
                minion.baseHealth = minion.vanillaHealth;

                return __instance.TrySummonMinion((Summon)minion, true);
            };
        }
    }
}
public class AttackBonus_Patch
{
    static void Postfix(Minion __instance, ref int __result)
    {
        if (EternalKnightRuntimeTracker.IsEternalKnight(__instance))
        {
            int oldSelfPassive = __instance.SelfAttackPassive();
            int deaths = EternalKnightRuntimeTracker.DeathsFor(__instance);
            int correctSelfPassive = deaths * (__instance.golden ? 8 : 4);
            __result = __result - oldSelfPassive + correctSelfPassive;
        }
        else if (AutomatonRuntimeTracker.IsAutomaton(__instance))
        {
            int oldSelfPassive = __instance.SelfAttackPassive();
            int otherSummons = AutomatonRuntimeTracker.OtherSummonsFor(__instance);
            int correctSelfPassive = otherSummons * (__instance.golden ? 6 : 3);
            __result = __result - oldSelfPassive + correctSelfPassive;
        }
    }
}

public class HealthBonus_Patch
{
    static void Postfix(Minion __instance, ref int __result)
    {
        if (EternalKnightRuntimeTracker.IsEternalKnight(__instance))
        {
            int oldSelfPassive = __instance.SelfHealthPassive();
            int deaths = EternalKnightRuntimeTracker.DeathsFor(__instance);
            int correctSelfPassive = deaths * (__instance.golden ? 4 : 2);
            __result = __result - oldSelfPassive + correctSelfPassive;
        }
        else if (AutomatonRuntimeTracker.IsAutomaton(__instance))
        {
            int oldSelfPassive = __instance.SelfHealthPassive();
            int otherSummons = AutomatonRuntimeTracker.OtherSummonsFor(__instance);
            int correctSelfPassive = otherSummons * (__instance.golden ? 4 : 2);
            __result = __result - oldSelfPassive + correctSelfPassive;
        }
    }
}