// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.VolatileVenom
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class VolatileVenom(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus,
  IOnFriendlyMinionAfterAttack
{
  public const string CardId = "BG24_Reward_364";
  public static int AttackBonus = 7;
  public static int HealthBonus = 7;

  public int PassiveAttackBonusFor(Minion minion) => VolatileVenom.AttackBonus;

  public int PassiveHealthBonusFor(Minion minion) => VolatileVenom.HealthBonus;

  public Action? OnFrienlyMinionAfterAttack(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      if (!attacker.IsAlive())
        return;
      this.Simulator.Destroy(attacker, (Entity) this);
    });
  }
}
