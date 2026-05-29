// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.StolenGold
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class StolenGold(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG24_Reward_109";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion first;
      Minion last;
      if (!this.FriendlySide.TryGetFirstAndLast<Minion>(out first, out last))
        return;
      first.TryMakeGolden(false, (Entity) this);
      last.TryMakeGolden(false, (Entity) this);
    });
  }
}
