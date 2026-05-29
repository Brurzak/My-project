// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Mishmash
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Mishmash(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_33_Buddy";
  public const string Text = "Whenever your Amalgam gains stats, this gains them too.";
  public const string GoldenText = "Whenever your Amalgam gains stats, this gains them twice.";

  public Action? OnFriendlyMinionBuffed(
    Minion minion,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return minion.CardID != "TB_BaconShop_HP_033t" ? (Action) null : (Action) (() =>
    {
      this.IncreaseStats(attackChange, healthChange);
      if (!this.golden)
        return;
      this.IncreaseStats(attackChange, healthChange);
    });
  }
}
