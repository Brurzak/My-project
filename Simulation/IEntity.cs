// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.IEntity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Enchantments;
using BobsBuddy.HeroPowers;
using BobsBuddy.Quests.Rewards;
using BobsBuddy.Spells;
using BobsBuddy.Trinkets;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public interface IEntity
{
  Simulator Simulator { get; }

  List<Minion> FriendlySide { get; }

  List<Minion>? TeammateSide { get; }

  List<CardEntity> FriendlyHand { get; }

  List<QuestReward> FriendlyQuestRewards { get; }

  List<Objective> FriendlyObjectives { get; }

  List<Trinket> FriendlyTrinkets { get; }

  List<HeroPower> FriendlyHeroPowers { get; }

  IEnumerable<Entity> FriendlyEntities { get; }

  List<Minion> OpposingSide { get; }

  List<QuestReward> OpposingQuestRewards { get; }

  List<Objective> OpposingObjectives { get; }

  List<HeroPower> OpposingHeroPowers { get; }

  IEnumerable<Entity> OpposingEntities { get; }

  string CardID { get; }

  bool ControlledByPlayer { get; }

  List<MinionCardEntity> FriendlyHandMinions(bool throwOnUnsupportedEntity = false);

  bool AddCardToFriendlyHand(CardEntity cardEntity);

  Action? OnMinionSummonedByFriendly(Minion summoned, Entity? source, bool wasReborn);

  string ToString();

  void AttachEnchantment(Enchantment enchantment);

  IEnumerable<T> GetTriggers<T>() where T : class;
}
