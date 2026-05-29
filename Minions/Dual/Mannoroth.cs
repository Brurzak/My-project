// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.Mannoroth
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class Mannoroth(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG27_507";
  public const string Text = "<b>Venomous.</b> After this kills a minion and survives, gain its maximum stats. <i>(Once per combat.)</i>";
  public const string GoldenText = "<b>Venomous.</b> After this kills a minion and survives, gain its maximum stats. <i>(Twice per combat.)</i>";
  private int _gainedStatsThisCombatCount;

  public Action? OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() =>
    {
      if (friendly != this || this.IsDead() || this._gainedStatsThisCombatCount >= this.DoubleIfGolden(1))
        return;
      this.IncreaseStats(killed.maxAttack + killed.attackBonus(), killed.maxHealth + killed.healthBonus(), (Entity) this);
      ++this._gainedStatsThisCombatCount;
    });
  }
}
