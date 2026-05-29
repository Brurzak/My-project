// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.GreaterBeetleBand
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Trinkets;

public class GreaterBeetleBand(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG32_MagicItem_860t";

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 6;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      Simulator simulator = this.Simulator;
      List<Summon> summonList = new List<Summon>();
      summonList.Add((Summon) "BG28_603t");
      summonList.Add((Summon) "BG28_603t");
      List<Minion> friendlySide = this.FriendlySide;
      int count = this.FriendlySide.Count;
      foreach (Minion trySummonMinion in simulator.TrySummonMinions((IEnumerable<Summon>) summonList, friendlySide, count, (Entity) this))
        trySummonMinion.taunt = true;
    });
  }
}
