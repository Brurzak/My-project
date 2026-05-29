// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.MechagonAdapter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class MechagonAdapter(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "BG30_MagicItem_910";
  private int _divineShieldCounter = 3;

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() =>
    {
      if (!(this._divineShieldCounter > 0 & (lost.IsMech() && lost.IsAlive())))
        return;
      lost.div = 1;
      --this._divineShieldCounter;
    });
  }
}
