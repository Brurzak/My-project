// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TimewarpedGreenskeeper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TimewarpedGreenskeeper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_Giant_041";
  public const string Text = "<b>Rally:</b> Trigger your right-most <b>Battlecry</b> and <b>Deathrattle</b>.";
  public const string GoldenText = "<b>Rally:</b> Trigger your right-most <b>Battlecry</b> and <b>Deathrattle</b> twice.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      for (int index = 0; index < (isGolden ? 2 : 1); ++index)
      {
        List<Minion> list1 = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.GetTriggers<IBattlecry>().Any<IBattlecry>())).ToList<Minion>();
        List<Minion> list2 = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.HasDeathrattle())).ToList<Minion>();
        if (list1.Any<Minion>())
        {
          Minion target1 = list1.Last<Minion>();
          minion.Simulator.InvokeBattlecry(target1);
        }
        if (list2.Any<Minion>())
        {
          this.Simulator.TriggerDeathrattles(list2.Last<Minion>());
          this.Simulator.ResolveTriggersInCurrentScope();
        }
      }
    });
  }
}
