// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BewitchedRibbon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BewitchedRibbon(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterAnySpellCast,
  IEntity
{
  public const string CardId = "BG35_MagicItem_923";

  public Action? OnAfterAnySpellCast(Entity? source, Minion? target)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion.IncreaseStats(3, 3);
    });
  }
}
