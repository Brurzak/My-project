// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Monstrosity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Monstrosity(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG20_HERO_282_Buddy";
  public const string Text = "After a friendly minion dies, gain its Attack.";
  public const string GoldenText = "After a friendly minion dies, gain its Attack twice.";

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    int attack = died.attack();
    return attack == 0 ? (Action) null : (Action) (() =>
    {
      this.IncreaseStats(attack, 0);
      if (!this.golden)
        return;
      this.IncreaseStats(attack, 0);
    });
  }
}
