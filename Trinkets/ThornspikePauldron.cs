// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.ThornspikePauldron
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class ThornspikePauldron(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyDeathrattle,
  IEntity
{
  public const string CardId = "BG35_MagicItem_431t";

  public Action? OnFriendlyDeathrattle()
  {
    return (Action) (() =>
    {
      GameState.PlayerState playerState = this.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent;
      playerState.BloodGemAtkBuff += 2;
      ++playerState.BloodGemHealthBuff;
    });
  }
}
