// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.EvilTwin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class EvilTwin(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG24_Reward_111";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count >= 7)
        return;
      Minion highestHealthMinion = this.Simulator.GetHighestHealthMinion(this.FriendlySide);
      if (highestHealthMinion == null)
        return;
      this.Simulator.TrySummonMinion((Summon) highestHealthMinion.CloneAsOriginal(), this.FriendlySide, highestHealthMinion.BoardPosition() + 1, (Entity) this);
    });
  }
}
