// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Lurker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class Lurker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG31_HERO_811t7";
  public const string Text = "<b><b>Stealth</b>. Avenge (1):</b> Gain +1/+1 permanently. <i>(Morphs each turn!)</i>1<b><b>Stealth</b>. Avenge (1):</b> Gain +1/+1 permanently.";
  public const string GoldenText = "<b><b>Stealth</b>. Avenge (1):</b> Gain +2/+2 permanently. <i>(Morphs each turn!)</i>1<b><b>Stealth</b>. Avenge (1):</b> Gain +2/+2 permanently.";

  public int AvengeRequirement => 1;

  public Action? OnAvenge()
  {
    return (Action) (() => this.AttachedOrThis.IncreaseStats(this.DoubleIfGolden(1)));
  }
}
