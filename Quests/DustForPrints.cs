// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.DustForPrints
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests;

public class DustForPrints(QuestData data, Simulator simulator, bool controlledByPlayer) : 
  Quest(data, simulator, controlledByPlayer),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG24_Quest_314";

  public Action OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    return (Action) (() => this.IncrementProgress());
  }
}
