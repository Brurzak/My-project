// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.DozyWhelp
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class DozyWhelp(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG24_300";
  public const string Text = "<b><b>Taunt</b>.</b> Whenever this is attacked, gain +1 Attack permanently.";
  public const string GoldenText = "<b><b>Taunt</b>.</b> Whenever this is attacked, gain +2 Attack permanently.";

  public override Action? OnIsAttacked(Minion attacker)
  {
    return (Action) (() => this.IncreaseStats(this.golden ? 2 : 1, 0));
  }
}
