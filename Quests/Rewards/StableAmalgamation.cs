// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.StableAmalgamation
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class StableAmalgamation(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity,
  IOnAfterAttackStep
{
  public const string CardId = "BG28_Reward_518";
  private int _count;

  public int AvengeCounter { get; set; }

  public int AvengeRequirement => 7;

  public Action? OnAvenge() => (Action) (() => ++this._count);

  public void OnAfterAttackStep()
  {
    for (; this._count > 0; --this._count)
    {
      List<Minion> minionList = this.Simulator.TrySummonMinion(new Summon("BG28_Reward_518t"), this.FriendlySide, this.FriendlySide.Count, (Entity) null);
      // ISSUE: explicit non-virtual call
      if ((minionList != null ? (__nonvirtual (minionList.Count) > 0 ? 1 : 0) : 0) == 0)
        break;
    }
  }
}
