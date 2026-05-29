// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.TheSmokingGun
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class TheSmokingGun(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity
{
  public const string CardId = "BG24_Reward_125";
  public static int AttackBonus = 4;

  public int PassiveAttackBonusFor(Minion target) => TheSmokingGun.AttackBonus;
}
