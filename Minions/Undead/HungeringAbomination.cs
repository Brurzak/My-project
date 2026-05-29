// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.HungeringAbomination
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class HungeringAbomination(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG25_014";
  public const string Text = "<b>Avenge (1):</b> Gain +1/+2 permanently.";
  public const string GoldenText = "<b>Avenge (1):</b> Gain +2/+4 permanently.";

  public int AvengeRequirement => 1;

  public int AttackGain => this.DoubleIfGolden(1);

  public int HealthGain => this.DoubleIfGolden(2);

  public Action? OnAvenge()
  {
    return (Action) (() => this.AttachedOrThis.IncreaseStats(this.AttackGain, this.HealthGain));
  }
}
