// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.RipsnarlCaptain
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class RipsnarlCaptain(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BGS_056";
  public const string Text = "Whenever a friendly Pirate attacks, give it +3 Attack.";
  public const string GoldenText = "Whenever a friendly Pirate attacks, give it +6 Attack.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      if (!attacker.IsPirate())
        return;
      attacker.IncreaseStats(this.DoubleIfGolden(3), 0, (Entity) this);
    });
  }
}
