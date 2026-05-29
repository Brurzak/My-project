// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.NerubianDeathswarmer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class NerubianDeathswarmer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_011";
  public const string Text = "<b>Battlecry:</b> Your Undead have +1 Attack this game <i>(wherever they are)</i>.";
  public const string GoldenText = "<b>Battlecry:</b> Your Undead have +2 Attack this game <i>(wherever they are)</i>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = this.golden ? 2 : 1;
      globalModifier.IncreaseUndeadAttackBonus(atkBuff, (Entity) this);
    });
  }
}
