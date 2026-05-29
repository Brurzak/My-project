// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.GreaterBirdFeeder
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class GreaterBirdFeeder(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG32_MagicItem_864t";

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 2;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(4);
    });
  }
}
