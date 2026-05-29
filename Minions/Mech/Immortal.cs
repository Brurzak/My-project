// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.Immortal
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class Immortal(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG31_HERO_802pt4";
  public const string Text = "<b>Start of Combat:</b> Gain the stats of adjacent minions.";
  public const string GoldenText = "<b>Start of Combat:</b> Gain double the stats of adjacent minions.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> list = new List<Minion>()
      {
        this.GetLeftNeighbor(),
        this.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null && x.IsAlive())).Cast<Minion>().ToList<Minion>();
      int i1 = 0;
      int i2 = 0;
      foreach (Minion minion in list)
      {
        i1 += minion.attack();
        i2 += minion.health();
      }
      this.IncreaseStats(this.DoubleIfGolden(i1), this.DoubleIfGolden(i2));
    });
  }
}
