// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.IcesnarlTheMighty
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class IcesnarlTheMighty(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG20_HERO_100_Buddy";
  public const string Text = "After a friendly minion kills an enemy, gain +3 Health permanently.";
  public const string GoldenText = "After a friendly minion kills an enemy, gain +6 Health permanently.";

  public Action OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() =>
    {
      if (this.Simulator.CurrentAttackTarget == this && this.IsDead())
        return;
      this.IncreaseStats(0, this.DoubleIfGolden(3));
    });
  }
}
