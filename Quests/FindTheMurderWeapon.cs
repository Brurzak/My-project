// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.FindTheMurderWeapon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Quests;

public class FindTheMurderWeapon(QuestData data, Simulator simulator, bool controlledByPlayer) : 
  Quest(data, simulator, controlledByPlayer),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG24_Quest_123";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (attackChange <= 0 && healthChange <= 0)
        return;
      this.IncrementProgress();
    });
  }
}
