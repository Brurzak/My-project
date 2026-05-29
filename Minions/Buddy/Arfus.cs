// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Arfus
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Arfus(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "TB_BaconShop_HERO_22_Buddy";
  public const string Text = "After a friendly minion is <b>Reborn</b>, give it this minion's Attack.";
  public const string GoldenText = "After a friendly minion is <b>Reborn</b>, give it this minion's Attack twice.";

  public override Action? OnMinionSummonedByFriendly(
    Minion summoned,
    Entity? source,
    bool wasReborn)
  {
    return !wasReborn ? (Action) null : (Action) (() => summoned.IncreaseStats(this.DoubleIfGolden(this.attack()), 0));
  }
}
