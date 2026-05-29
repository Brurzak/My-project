// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.HunterOfGatherers
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class HunterOfGatherers(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG25_027";
  public const string Text = "Whenever this gains Attack, give your minions +2 Health.";
  public const string GoldenText = "Whenever this gains Attack, give your minions +4 Health.";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (buffed != this || attackChange <= 0)
        return;
      foreach (Minion minion in buffed.FriendlySide)
        minion.IncreaseStats(0, this.DoubleIfGolden(2));
    });
  }
}
