// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.GilneanThornedRose
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class GilneanThornedRose(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG30_MagicItem_864";
  private const int StatusBonus = 3;

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      if (list.Count == 0)
        return;
      foreach (Minion minion in list)
        minion.IncreaseStats(3);
      this.Simulator.ProcessDamage(list.Select<Minion, Damage>((Func<Minion, Damage>) (m => new Damage(1, m, (Entity) this))));
    });
  }
}
