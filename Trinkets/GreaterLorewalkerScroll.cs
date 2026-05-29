// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.GreaterLorewalkerScroll
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class GreaterLorewalkerScroll(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterAnySpellCast,
  IEntity
{
  public const string CardId = "BG30_MagicItem_422t";

  public Action? OnAfterAnySpellCast(Entity? source, Minion? target)
  {
    return (Action) (() =>
    {
      if (target == null || !target.IsAlive())
        return;
      target.IncreaseStats(7, 7);
    });
  }
}
