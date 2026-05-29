// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.RoundUpTheSuspects
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests;

public class RoundUpTheSuspects(QuestData data, Simulator simulator, bool controlledByPlayer) : 
  Quest(data, simulator, controlledByPlayer),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG27_Quest_802";

  public Action? OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() => this.IncrementProgress());
  }
}
