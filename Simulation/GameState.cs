// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.GameState
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.HeroPowers;
using BobsBuddy.Quests;
using BobsBuddy.Quests.Rewards;
using BobsBuddy.Spells;
using BobsBuddy.Trinkets;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public class GameState
{
  public void SetPlayerCounters(GameState.PlayerState player, BobsBuddy.Simulation.Player inputPlayer)
  {
    player.FriendlyMinionsDeadLastCombatCounter = inputPlayer.FriendlyMinionsDeadLastCombatCounter;
    player.EternalKnightCounter = inputPlayer.EternalKnightCounter;
    player.ElementalPlayCounter = inputPlayer.ElementalPlayCounter;
    player.BloodGemAtkBuff = inputPlayer.BloodGemAtkBuff;
    player.BloodGemHealthBuff = inputPlayer.BloodGemHealthBuff;
    player.BeastAttackBonus = inputPlayer.BeastAttackBonus;
    player.BeastHealthBonus = inputPlayer.BeastHealthBonus;
    player.PiratesSummonCounter = inputPlayer.PiratesSummonCounter;
    player.ResourcesSpentThisGame = inputPlayer.ResourcesSpentThisGame;
    player.BeastsSummonCounter = inputPlayer.BeastsSummonCounter;
    player.BeetlesAtkBuff = inputPlayer.BeetlesAtkBuff;
    player.BeetlesHealthBuff = inputPlayer.BeetlesHealthBuff;
    player.BattlecryCounter = inputPlayer.BattlecryCounter;
    player.DeathrattleCounter = inputPlayer.DeathrattleCounter;
    player.SanlaynScribeCounter = inputPlayer.SanlaynScribeCounter;
    player.AncestralAutomatonCounter = inputPlayer.AncestralAutomatonCounter;
    player.DeepBluesCounter = inputPlayer.DeepBluesCounter;
    player.TavernSpellCounter = inputPlayer.TavernSpellCounter;
    player.AnySpellCounter = inputPlayer.AnySpellCounter;
    player.VolumizerAtkBuff = inputPlayer.VolumizerAtkBuff;
    player.VolumizerHealthBuff = inputPlayer.VolumizerHealthBuff;
    player.TavernSpellAtkBuff = inputPlayer.TavernSpellAtkBuff;
    player.TavernSpellHealthBuff = inputPlayer.TavernSpellHealthBuff;
    foreach (Minion minion in inputPlayer.Side)
    {
      if (minion is IOnSetupPlayerStateCounters playerStateCounters)
        playerStateCounters.OnSetupPlayerStateCounters(player);
    }
  }

  public GameState(Input? input)
  {
    if (input == null)
      return;
    this.SetPlayerCounters(this.Player, input.Player);
    this.SetPlayerCounters(this.Opponent, input.Opponent);
  }

  public bool IsStartOfCombatResolved { get; set; }

  public IReadOnlyList<Minion>? ActiveSide { get; set; }

  public GameState.PlayerState Player { get; } = new GameState.PlayerState();

  public GameState.PlayerState Opponent { get; } = new GameState.PlayerState();

  public List<RebornTrigger> Reborn { get; } = new List<RebornTrigger>();

  public Scope<List<Trigger>> TriggerScope { get; } = new Scope<List<Trigger>>("Trigger");

  public Scope<SummonCounters> SummonScope { get; } = new Scope<SummonCounters>("Summon");

  public class PlayerState
  {
    public int Tier { get; set; } = 1;

    public int Health { get; set; }

    public int FriendlyMinionsDeadLastCombatCounter { get; set; }

    public int EternalKnightCounter { get; set; }

    public int ElementalPlayCounter { get; set; }

    public int BloodGemAtkBuff { get; set; }

    public int BloodGemHealthBuff { get; set; } = 1;

    public int PiratesSummonCounter { get; set; }

    public int ResourcesSpentThisGame { get; set; }

    public int BeastsSummonCounter { get; set; }

    public int BeastAttackBonus { get; set; }

    public int BeastHealthBonus { get; set; }

    public int BeetlesAtkBuff { get; set; }

    public int BeetlesHealthBuff { get; set; }

    public int SanlaynScribeCounter { get; set; }

    public int BattlecryCounter { get; set; }

    public int DeathrattleCounter { get; set; }

    public int AncestralAutomatonCounter { get; set; }

    public int DeepBluesCounter { get; set; }

    public int TavernSpellCounter { get; set; }

    public int AnySpellCounter { get; set; }

    public int VolumizerAtkBuff { get; set; }

    public int VolumizerHealthBuff { get; set; }

    public int TavernSpellAtkBuff { get; set; }

    public int TavernSpellHealthBuff { get; set; }

    public int HauntedAtkBuff { get; set; }

    public int HauntedHealthBuff { get; set; }

    public GlobalModifier? GlobalModifier { get; set; }

    public List<HeroPower> HeroPowers { get; } = new List<HeroPower>();

    public List<Objective> Objectives { get; } = new List<Objective>();

    public List<Trinket> Trinkets { get; } = new List<Trinket>();

    public List<Quest> Quests { get; } = new List<Quest>();

    public List<QuestReward> QuestRewards { get; } = new List<QuestReward>();

    public Minion? SideLastAttacker { get; set; }

    public List<Minion> Side { get; } = new List<Minion>();

    public List<Minion> DeadMinions { get; } = new List<Minion>();

    public List<Minion> SummonedMinions { get; } = new List<Minion>();

    public List<Simulator.Secrets> Secrets { get; } = new List<Simulator.Secrets>();

    public List<CardEntity> Hand { get; } = new List<CardEntity>();

    public HashSet<Minion> SummonedByMoose { get; } = new HashSet<Minion>();

    public int HandSize => this.Hand.Count;
  }
}
