// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.FillTheCauldron
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests;

public class FillTheCauldron(QuestData data, Simulator simulator, bool controlledByPlayer) : 
  Quest(data, simulator, controlledByPlayer),
  IOnAfterAnySpellCast,
  IEntity
{
  public const string CardId = "BG28_Quest_500";

  public Action? OnAfterAnySpellCast(Entity? source, Minion? target)
  {
    return (Action) (() => this.IncrementProgress());
  }
}
