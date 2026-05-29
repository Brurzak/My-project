// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.FangAnklet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class FangAnklet(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG35_MagicItem_701";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int num = Math.Max(this.ScriptDataNum1, 1);
      (this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier)?.IncreaseBeastBonus(num, num, (Entity) this);
    });
  }
}
