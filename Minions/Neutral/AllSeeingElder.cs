// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.AllSeeingElder
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class AllSeeingElder(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG33_300";
  public const string Text = "<b>Start of Combat:</b> Gain the Attack of your left-most minion and the Health of your right-most minion.";
  public const string GoldenText = "<b>Start of Combat:</b> Gain the Attack of your left-most minion and the Health of your right-most minion twice.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count == 0)
        return;
      Minion minion1 = this.FriendlySide[0];
      Minion minion2 = this.FriendlySide.Count >= 1 ? this.FriendlySide.Last<Minion>() : (Minion) null;
      int i1 = 0;
      int i2 = 0;
      if (minion1.IsAlive())
        i1 = minion1.attack();
      if (minion2 != null && minion2.IsAlive())
        i2 = minion2.health();
      this.IncreaseStats(this.DoubleIfGolden(i1), this.DoubleIfGolden(i2));
    });
  }
}
