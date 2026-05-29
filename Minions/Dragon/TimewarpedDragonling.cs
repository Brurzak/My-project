// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TimewarpedDragonling
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TimewarpedDragonling(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_029";
  public const string Text = "<b>Start of Combat:</b> Give this minion and its neighbors stats equal to your Tier.";
  public const string GoldenText = "<b>Start of Combat:</b> Give this minion and its neighbors stats equal to twice your Tier.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int by = this.DoubleIfGolden(this.ControlledByPlayer ? this.Simulator.PlayerState.Tier : this.Simulator.OpponentState.Tier);
      foreach (Minion minion in new List<Minion>()
      {
        (Minion) this,
        this.GetLeftNeighbor(),
        this.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null)).Cast<Minion>())
        minion.IncreaseStats(by);
    });
  }
}
