// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.HummingBird
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class HummingBird(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG26_805";
  public const string Text = "<b>Start of Combat:</b> For the rest of this combat, your Beasts have +1 Attack.";
  public const string GoldenText = "<b>Start of Combat:</b> For the rest of this combat, your Beasts have +2 Attack.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = this.DoubleIfGolden(1);
      globalModifier.IncreaseBeastBonus(atkBuff, 0, (Entity) this);
    });
  }
}
