// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.StalwartKodo
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class StalwartKodo(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMinionMadeGolden,
  IEntity,
  IOnAfterFriendlyMinionSummoned
{
  public const string CardId = "BG34_322";
  public const string Text = "After you summon a minion in combat, give it this minion's maximum stats. <i>(3 times per combat.)</i>3[x]After you summon a minion in combat, give it this minion's maximum stats. <i>({0} left!)</i>";
  public const string GoldenText = "After you summon a minion in combat, give it double this minion's maximum stats. <i>(3 times per combat.)</i>3[x]After you summon a minion in combat, give it double this minion's maximum stats. <i>({0} left!)</i>";
  private int _remainingTriggers = 3;

  public Action? OnMinionMadeGolden() => (Action) (() => this._remainingTriggers = 3);

  public Action OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (this._remainingTriggers <= 0 || summoned.IsDead())
        return;
      --this._remainingTriggers;
      summoned.IncreaseStats(this.DoubleIfGolden(this.maxAttack), this.DoubleIfGolden(this.maxHealth));
    });
  }
}
