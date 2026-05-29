// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BuddingGreenthumb
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class BuddingGreenthumb(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG21_030";
  public const string Text = "<b>Avenge ({2}):</b> Give adjacent minions +{0}/+{1} permanently.";
  public const string GoldenText = "<b>Avenge ({2}):</b> Give adjacent minions +{0}/+{1} permanently.";

  public int AvengeRequirement => 3;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      this.GetLeftNeighbor()?.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(2));
      this.GetRightNeighbor()?.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(2));
    });
  }
}
