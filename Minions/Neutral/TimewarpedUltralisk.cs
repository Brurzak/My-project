// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TimewarpedUltralisk
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class TimewarpedUltralisk(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Treasure_994";
  public const string Text = "Also damages adjacent minions. <b>Start of Combat:</b> Double this minion's stats.";
  public const string GoldenText = "Also damages adjacent minions. <b>Start of Combat:</b> Triple this minion's stats.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      this.IncreaseStats(this.attack() * num, this.health() * num);
    });
  }
}
