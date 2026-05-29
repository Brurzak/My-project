// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.FaerieDragonScale
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class FaerieDragonScale(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG32_MagicItem_363";
  private int _availableTriggers = 3;

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      if (!attacker.IsDragon() || this._availableTriggers <= 0 || attacker.hasDiv)
        return;
      attacker.div = 1;
      --this._availableTriggers;
    });
  }
}
