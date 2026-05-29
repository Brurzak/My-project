// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BubbleCrown
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BubbleCrown(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterAnySpellCast,
  IEntity
{
  public const string CardId = "BG35_MagicItem_920";

  public Action? OnAfterAnySpellCast(Entity? source, Minion? target)
  {
    return (Action) (() =>
    {
      ++this.ScriptDataNum1;
      if (this.ScriptDataNum1 % 10 != 0)
        return;
      GameState.PlayerState playerState = this.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent;
      playerState.TavernSpellAtkBuff += 3;
      playerState.TavernSpellHealthBuff += 3;
    });
  }
}
