// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.TitanicGuardian
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class TitanicGuardian(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity,
  IOnFriendlyHandMinionBuffed
{
  public const string CardId = "TB_BaconShop_HERO_39_Buddy";
  public const string Text = "Whenever a different minion in your hand or board gains Health, this also gains it.";
  public const string GoldenText = "Whenever a different minion in your hand or board gains Health, this also gains twice that amount.";

  private void ApplyHealthGain(Minion buffed, int healthChange)
  {
    if (buffed.CardID == "TB_BaconShop_HERO_39_Buddy" || healthChange <= 0)
      return;
    this.IncreaseStats(0, this.DoubleIfGolden(healthChange));
  }

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() => this.ApplyHealthGain(buffed, healthChange));
  }

  public Action? OnFriendlyHandMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() => this.ApplyHealthGain(buffed, healthChange));
  }
}
