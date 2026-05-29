// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BlingtronsSunglasses
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

public class BlingtronsSunglasses(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer)
{
  public const string CardId = "BG30_MagicItem_978";

  public override Action? OnMinionSummonedByFriendly(
    Minion summoned,
    Entity? source,
    bool wasReborn)
  {
    return (Action) (() =>
    {
      if (!summoned.IsMech())
        return;
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsMech() && m.IsAlive() && !m.hasDiv)).ToList<Minion>();
      Minion minion;
      if (list.Count == 0 || !list.TryGetRandom<Minion>(out minion))
        return;
      minion.div = 1;
    });
  }
}
