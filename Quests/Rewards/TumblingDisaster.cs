// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.TumblingDisaster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class TumblingDisaster(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity,
  IOnFriendlyMinionSummoned
{
  public const string CardId = "BG28_Reward_505";
  private int _avengeTriggers;

  public int AvengeCounter { get; set; }

  public int AvengeRequirement => 4;

  public Action? OnAvenge() => (Action) (() => ++this._avengeTriggers);

  public int StartBuff => this.ScriptDataNum1;

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsAlive())
        return;
      summoned.IncreaseStats(this.StartBuff + this._avengeTriggers);
    });
  }
}
