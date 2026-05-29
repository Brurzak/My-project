// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.RedChromadrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class RedChromadrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_638t";
  public const string Text = "<b>Battlecry:</b> Your Tavern spells give an extra +{0} Attack this game.";
  public const string GoldenText = "<b>Battlecry:</b> Your Tavern spells give an extra +{0} Attack this game.";

  public Action? OnBattlecry()
  {
    return (Action) (() => (this.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent).TavernSpellAtkBuff += this.DoubleIfGolden(1));
  }
}
