// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.OnyxiaHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class OnyxiaHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IAvenge, IEntity
{
  private int _combatBuff;

  public int AvengeCounter { get; set; }

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      Simulator simulator = this.Simulator;
      Summon summon = new Summon("BG22_HERO_305t");
      summon.SetStats = new (int, int)?((this.Data.Data + this._combatBuff, this.Data.Data + this._combatBuff));
      List<Minion> friendlySide = this.FriendlySide;
      int count = this.FriendlySide.Count;
      simulator.TrySummonMinion(summon, friendlySide, count, (Entity) this);
      ++this._combatBuff;
    });
  }
}
