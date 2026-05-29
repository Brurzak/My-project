// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.OranomonosTheWilted
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class OranomonosTheWilted(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionReborn,
  IEntity
{
  public const string CardId = "BG33_116";
  public const string Text = "After a friendly minion is <b>Reborn</b>, your Undead have +{0} Attack this game <i>(wherever they are)</i>.";
  public const string GoldenText = "After a friendly minion is <b>Reborn</b>, your Undead have +{0} Attack this game <i>(wherever they are)</i>.";

  public Action? OnAfterFriendlyMinionReborn(Minion minion)
  {
    return (Action) (() =>
    {
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = this.DoubleIfGolden(2);
      globalModifier.IncreaseUndeadAttackBonus(atkBuff, (Entity) minion);
    });
  }
}
