// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BolvarFireblood
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class BolvarFireblood(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "ICC_858";
  public const string Text = "<b>Divine Shield</b> After a friendly minion loses <b>Divine Shield</b>, gain +2 Attack.";

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(2), 0));
  }
}
