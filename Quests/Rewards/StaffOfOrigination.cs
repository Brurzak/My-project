// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.StaffOfOrigination
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class StaffOfOrigination(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG24_Reward_312";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide)
        minion.IncreaseStats(12, 12);
    });
  }
}
