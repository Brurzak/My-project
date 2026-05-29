// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Sinestra
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Sinestra(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_52_Buddy";
  public const string Text = "Whenever a friendly minion gains Attack during combat, give it +1 Health permanently.";
  public const string GoldenText = "Whenever a friendly minion gains Attack during combat, give it +2 Health permanently.";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (attackChange <= 0)
        return;
      buffed.IncreaseStats(0, this.DoubleIfGolden(1));
    });
  }
}
