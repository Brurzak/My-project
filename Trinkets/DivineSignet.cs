// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.DivineSignet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class DivineSignet(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "BG32_MagicItem_171";
  private int _triggerCounter = 4;

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() =>
    {
      if (this._triggerCounter <= 0)
        return;
      this.AddSpellToFriendlyHand();
      --this._triggerCounter;
    });
  }
}
