// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.MisfitDragonling
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class MisfitDragonling(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG29_814";
  public const string Text = "<b>Start of Combat:</b> Gain stats equal to your Tier.";
  public const string GoldenText = "<b>Start of Combat:</b> Gain stats equal to double your Tier.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(this.ControlledByPlayer ? this.Simulator.PlayerState.Tier : this.Simulator.OpponentState.Tier)));
  }
}
