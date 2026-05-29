// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.AdmiralElizaGoreblade
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class AdmiralElizaGoreblade(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG27_555";
  public const string Text = "Whenever a friendly Pirate attacks, give all friendly minions +3/+1.";
  public const string GoldenText = "Whenever a friendly Pirate attacks, give all friendly minions +6/+2.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      if (!attacker.IsPirate())
        return;
      foreach (Minion minion in this.FriendlySide)
        minion.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(1), (Entity) this);
    });
  }
}
