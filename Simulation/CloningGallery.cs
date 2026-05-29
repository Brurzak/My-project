// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.CloningGallery
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Quests;
using BobsBuddy.Quests.Rewards;
using BobsBuddy.Spells;
using BobsBuddy.Trinkets;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public class CloningGallery
{
  public List<Minion> Minions { get; } = new List<Minion>();

  public List<Trinket> Trinkets { get; } = new List<Trinket>();

  public List<Quest> Quests { get; } = new List<Quest>();

  public List<QuestReward> QuestRewards { get; } = new List<QuestReward>();

  public List<Objective> Objectives { get; } = new List<Objective>();

  public List<CardEntity> Hand { get; } = new List<CardEntity>();

  public List<Simulator.Secrets> Secrets { get; } = new List<Simulator.Secrets>();
}
