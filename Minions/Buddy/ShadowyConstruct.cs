// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.ShadowyConstruct
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class ShadowyConstruct(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMinionMadeGolden,
  IEntity,
  IOnFriendlyMinionDied
{
  public const string CardId = "BG25_HERO_103_Buddy";
  public const string Text = "After a friendly minion dies, gain its maximum stats. <i>(Once per combat.)</i>";
  public const string GoldenText = "After a friendly minion dies, gain its maximum stats. <i>(Twice per combat.)</i>";
  private int _timesActivated;

  public Action? OnMinionMadeGolden() => (Action) (() => this._timesActivated = 0);

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (this._timesActivated >= this.DoubleIfGolden(1))
        return;
      this.IncreaseStats(died.maxAttack, died.maxHealth);
      ++this._timesActivated;
    });
  }
}
