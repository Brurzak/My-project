// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.RelentlessMurghoul
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class RelentlessMurghoul(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG27_010";
  public const string Text = "<b>Venomous</b> <b>Avenge (5):</b> Gain <b>Reborn</b>.";
  public const string GoldenText = "<b>Venomous</b> <b>Avenge (5):</b> Gain <b>Reborn</b>.";

  public int AvengeRequirement => 5;

  public Action? OnAvenge() => (Action) (() => this.reborn = true);
}
