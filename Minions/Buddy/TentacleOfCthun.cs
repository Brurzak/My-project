// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.TentacleOfCthun
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class TentacleOfCthun(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_29_Buddy";
  public const string Text = "After a different friendly minion gains stats, gain +1/+1 until next turn.";
  public const string GoldenText = "After a different friendly minion gains stats, gain +2/+2 until next turn.";

  public Action? OnFriendlyMinionBuffed(
    Minion minion,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return minion.CardID == "TB_BaconShop_HERO_29_Buddy" ? (Action) null : (Action) (() => this.IncreaseStats(this.golden ? 2 : 1));
  }
}
