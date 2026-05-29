// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.ToxicTumbleweed
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells;

public class ToxicTumbleweed(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG28_641";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> source = this.Simulator.TrySummonMinion((Summon) "BG28_641t", this.FriendlySide, this.FriendlySide.Count, (Entity) this);
      if (source.Count != 1)
        return;
      this.Simulator.AttackWithMinion(source.Single<Minion>());
    });
  }
}
