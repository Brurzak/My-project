// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.GreaseBot
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class GreaseBot(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "BG21_024";
  public const string Text = "<b>Divine Shield</b> After a friendly minion loses <b>Divine Shield</b>, give it +2/+2 permanently.";
  public const string GoldenText = "<b>Divine Shield</b> After a friendly minion loses <b>Divine Shield</b>, give it +4/+4 permanently.";

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() =>
    {
      if (!lost.IsAlive())
        return;
      lost.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(2));
    });
  }
}
