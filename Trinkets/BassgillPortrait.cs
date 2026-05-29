// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BassgillPortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BassgillPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG32_MagicItem_301";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsMurloc() || !summoned.IsAlive())
        return;
      summoned.div = 1;
    });
  }
}
