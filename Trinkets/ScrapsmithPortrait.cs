// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.ScrapsmithPortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class ScrapsmithPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG35_MagicItem_430";

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.taunt)
        return;
      foreach (Minion target in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.CardID == "BG24_707")).ToList<Minion>())
        this.Simulator.CastBloodGem(target, (Entity) this);
    });
  }
}
