// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BloodAmulet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BloodAmulet(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyDeathrattle,
  IEntity
{
  public const string CardId = "BG35_MagicItem_432";

  public Action? OnFriendlyDeathrattle()
  {
    return (Action) (() =>
    {
      foreach (Minion randomElement in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().GetRandomElements<Minion>(3))
        this.Simulator.CastBloodGem(randomElement, (Entity) this);
    });
  }
}
