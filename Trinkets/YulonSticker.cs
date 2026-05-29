// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.YulonSticker
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

public class YulonSticker(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG32_MagicItem_419";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<IGrouping<int, Minion>> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsDragon())).ToList<Minion>().GroupBy<Minion, int>((Func<Minion, int>) (x => x.tier)).ToList<IGrouping<int, Minion>>();
      Minion minion;
      if (!list.Any<IGrouping<int, Minion>>() || !list.OrderBy<IGrouping<int, Minion>, int>((Func<IGrouping<int, Minion>, int>) (x => x.Key)).Last<IGrouping<int, Minion>>().ToList<Minion>().TryGetRandom<Minion>(out minion))
        return;
      minion.TryMakeGolden(true, (Entity) this);
    });
  }
}
