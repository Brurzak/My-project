// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.ReinforcedShield
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class ReinforcedShield(string cardId, Simulator simulator, bool controlledByPlayer) : Trinket(cardId, simulator, controlledByPlayer)
{
  public const string CardId = "BG30_MagicItem_886";
  private int _divineShieldCounter = 5;

  public override Action? OnMinionSummonedByFriendly(
    Minion summoned,
    Entity? source,
    bool wasReborn)
  {
    return (Action) (() =>
    {
      if (!summoned.IsAlive() || summoned.hasDiv || this._divineShieldCounter <= 0)
        return;
      summoned.div = 1;
      --this._divineShieldCounter;
    });
  }
}
