// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.RelentlessSentry
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class RelentlessSentry(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG25_003";
  public const string Text = "<b>Avenge (4):</b> Gain <b>Taunt</b> and <b><b>Reborn</b>.</b>";
  public const string GoldenText = "<b>Avenge (4):</b> Gain <b>Taunt</b> and <b><b>Reborn</b>.</b>";

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      this.taunt = true;
      this.reborn = true;
    });
  }
}
