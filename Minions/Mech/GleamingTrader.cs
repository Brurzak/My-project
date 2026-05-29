// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.GleamingTrader
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class GleamingTrader(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "BG33_805";
  public const string Text = "<b>Divine Shield</b>. After a friendly minion loses <b>Divine Shield</b>, your minions have +{0} Attack for the rest of this combat.";
  public const string GoldenText = "<b>Divine Shield</b>. After a friendly minion loses <b>Divine Shield</b>, your minions have +{0} Attack for the rest of this combat.";

  public Action? OnFriendlyMinionLostDiv(Minion minion)
  {
    return (Action) (() =>
    {
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = this.DoubleIfGolden(3);
      globalModifier.IncreaseAttackBonus(atkBuff, (Entity) this);
    });
  }
}
