// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.CloudSerpentHorn
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class CloudSerpentHorn(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG35_MagicItem_849";

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 3;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count < 2)
        return;
      Minion rightMost = this.FriendlySide.Last<Minion>();
      Minion minion;
      if (rightMost.IsDead() || rightMost.attack() == 0 || !this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsDragon() && x != rightMost)).ToList<Minion>().TryGetRandom<Minion>(out minion))
        return;
      minion.IncreaseStats(rightMost.attack(), 0);
    });
  }
}
