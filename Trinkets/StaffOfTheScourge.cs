// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.StaffOfTheScourge
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class StaffOfTheScourge(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG30_MagicItem_437";

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 5;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && !x.reborn)).ToList<Minion>();
      Minion minion;
      if (list.Count == 0 || !list.TryGetRandom<Minion>(out minion))
        return;
      minion.reborn = true;
    });
  }
}
