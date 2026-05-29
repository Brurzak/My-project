// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.ElectricSynthesizer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class ElectricSynthesizer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity,
  IBattlecry
{
  public const string CardId = "BG26_963";
  public const string Text = "<b>Battlecry and Start of Combat:</b> Give your other Dragons +1/+1.";
  public const string GoldenText = "<b>Battlecry and Start of Combat:</b> Give your other Dragons +2/+2.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int by = this.DoubleIfGolden(1);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive() && x.IsDragon())))
        minion.IncreaseStats(by);
    });
  }

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int by = this.DoubleIfGolden(1);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive() && x.IsDragon())))
        minion.IncreaseStats(by);
    });
  }
}
