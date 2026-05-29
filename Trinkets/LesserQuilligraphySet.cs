// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserQuilligraphySet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserQuilligraphySet(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG30_MagicItem_410";
  private const int StatusBonus = 1;

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      if (this.ControlledByPlayer)
        ++this.Simulator.state.Player.BloodGemHealthBuff;
      else
        ++this.Simulator.state.Opponent.BloodGemHealthBuff;
    });
  }
}
