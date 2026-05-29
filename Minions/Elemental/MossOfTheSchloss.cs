// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.MossOfTheSchloss
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class MossOfTheSchloss(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG30_111";
  public const string Text = "When another friendly Elemental dies, gain its maximum stats. <i>(Once per combat)</i>.";
  public const string GoldenText = "When another friendly Elemental dies, gain its maximum stats. <i>(Twice per combat)</i>.";
  private int _triggers;

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsElemental() || this._triggers++ >= (this.golden ? 2 : 1))
        return;
      this.IncreaseStats(died.maxAttack + died.attackBonus(), died.maxHealth + died.healthBonus(), (Entity) this);
    });
  }
}
