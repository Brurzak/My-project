// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserBeetleBand
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserBeetleBand(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG32_MagicItem_860";

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 5;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.Simulator.TrySummonMinion((Summon) "BG28_603t", this.FriendlySide, this.FriendlySide.Count, (Entity) this))
        minion.taunt = true;
    });
  }
}
