// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedSaurolisk
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedSaurolisk(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_202";
  public const string Text = "After you trigger a <b>Deathrattle</b>, gain +{0}/+{1} permanently.";
  public const string GoldenText = "After you trigger a <b>Deathrattle</b>, gain +{0}/+{1} permanently.";

  public Action? OnFriendlyDeathrattle()
  {
    return (Action) (() =>
    {
      if (!this.IsAlive())
        return;
      this.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(2));
    });
  }
}
