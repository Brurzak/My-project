// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.MugOfTheSire
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class MugOfTheSire(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionFailedToSummonNoSpace,
  IEntity
{
  public const string CardId = "BG30_MagicItem_438t";

  public Action? OnFriendlyMinionFailedToSummonNoSpace(Summon summoned)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(5, 0);
    });
  }
}
