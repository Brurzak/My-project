// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BloodGolemSticker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BloodGolemSticker(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG30_MagicItem_442";
  private int _summoned;

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsQuilboar() || this._summoned >= 3)
        return;
      (int num3, int num4) = died.StatsFromBloodGems;
      if (num4 <= 0)
        return;
      died.TrySummonMinion(new Summon("BG30_MagicItem_442t")
      {
        SetStats = new (int, int)?((num3, num4))
      });
      ++this._summoned;
    });
  }
}
