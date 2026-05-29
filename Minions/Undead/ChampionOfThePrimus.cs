// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.ChampionOfThePrimus
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class ChampionOfThePrimus(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG27_029";
  public const string Text = "<b>Avenge ({1}):</b> Your Undead have +{0} Attack this game <i>(wherever they are)</i>.";
  public const string GoldenText = "<b>Avenge ({1}):</b> Your Undead have +{0} Attack this game <i>(wherever they are)</i>.";

  public int AvengeRequirement => 2;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = this.DoubleIfGolden(1);
      globalModifier.IncreaseUndeadAttackBonus(atkBuff, (Entity) this);
    });
  }
}
