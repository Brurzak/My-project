// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Greenskeeper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class Greenskeeper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG30_008";
  public const string Text = "<b>Rally:</b> Trigger your right-most <b>Battlecry</b>.";
  public const string GoldenText = "<b>Rally:</b> Trigger your right-most <b>Battlecry</b> twice.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.GetTriggers<IBattlecry>().Any<IBattlecry>())).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      Minion target1 = list.Last<Minion>();
      minion.Simulator.InvokeBattlecry(target1);
      if (!isGolden)
        return;
      minion.Simulator.InvokeBattlecry(target1);
    });
  }
}
