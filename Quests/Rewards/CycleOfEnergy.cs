// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.CycleOfEnergy
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class CycleOfEnergy(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG28_Reward_504";

  public int AvengeCounter { get; set; }

  public int AvengeRequirement => 3;

  public Action? OnAvenge() => (Action) (() => this.AddSpellToFriendlyHand());
}
