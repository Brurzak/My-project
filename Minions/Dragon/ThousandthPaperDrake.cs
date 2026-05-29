// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.ThousandthPaperDrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class ThousandthPaperDrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG29_810";
  public const string Text = "<b>Start of Combat:</b> Give your left-most Dragon +1/+2 and <b>Windfury</b>.";
  public const string GoldenText = "<b>Start of Combat:</b> Give your two left-most Dragons +1/+2 and <b>Windfury</b>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      IEnumerable<Minion> source = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsDragon()));
      if (!source.Any<Minion>())
        return;
      foreach (Minion minion in source.Take<Minion>(this.DoubleIfGolden(1)).ToList<Minion>())
      {
        minion.windfury = true;
        minion.IncreaseStats(1, 2);
      }
    });
  }
}
