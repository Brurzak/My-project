// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.TimewarpedGreaseBot
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class TimewarpedGreaseBot(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "BG34_Giant_656";
  public const string Text = "<b>Divine Shield</b>. After a friendly minion loses <b>Divine Shield</b>, give your minions +{0}/+{1} permanently.";
  public const string GoldenText = "<b>Divine Shield</b>. After a friendly minion loses <b>Divine Shield</b>, give your minions +{0}/+{1} permanently.";

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide)
      {
        if (minion.IsAlive())
          minion.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(3));
      }
    });
  }
}
