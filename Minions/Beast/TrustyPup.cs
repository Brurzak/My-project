// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TrustyPup
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TrustyPup(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG29_800";
  public const string Text = "Whenever this takes damage, gain +1 Attack permanently.";
  public const string GoldenText = "Whenever this takes damage, gain +2 Attack permanently.";

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1), 0));
  }
}
