// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserComfyCoffin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserComfyCoffin(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterTavernSpellCast,
  IEntity
{
  public const string CardId = "BG30_MagicItem_547";

  public Action? OnAfterTavernSpellCast(Minion? target)
  {
    return (Action) (() => (this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier)?.IncreaseUndeadAttackBonus(1, (Entity) this));
  }
}
