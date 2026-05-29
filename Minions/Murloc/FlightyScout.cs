// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.FlightyScout
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class FlightyScout(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity,
  ISummonFromHand
{
  public const string CardId = "BG32_330";
  public const string Text = "<b>Start of Combat:</b> If this minion is in your hand, summon a copy of it.";
  public const string GoldenText = "<b>Start of Combat:</b> If this minion is in your hand, summon a copy of it with double stats.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      this.IsPendingSummon = true;
      this.TrySummonFromHand();
    });
  }

  public bool IsPendingSummon { get; set; }

  private void TrySummonFromHand()
  {
    if (!this.IsPendingSummon || !(this.FriendlyHand.FirstOrDefault<CardEntity>((Func<CardEntity, bool>) (x => x is MinionCardEntity minionCardEntity1 && minionCardEntity1.Data == this)) is MinionCardEntity minionCardEntity2) || !minionCardEntity2.CanSummon)
      return;
    int num = this.DoubleIfGolden(1);
    Minion minion = this.Clone();
    minion.SetStats(new int?(this.attack() * num), new int?(this.health() * num));
    this.Simulator.TrySummonMinion((Summon) minion, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
    Scope<SummonCounters>.ScopeData current = this.Simulator.state.SummonScope.Current;
    (this.FriendlySide == this.Simulator.playerSide ? current.Data.Player : current.Data.Opponent).Clear();
  }
}
