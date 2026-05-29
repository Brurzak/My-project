// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.YulonFortuneGranter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class YulonFortuneGranter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG29_811";
  public const string Text = "<b>Start of Combat:</b> Make your lowest-Tier minion Golden <i>(except Yu'lon, Fortune Granter)</i>.";
  public const string GoldenText = "<b>Start of Combat:</b> Make your two lowest-Tier minions Golden <i>(except Yu'lon, Fortune Granter)</i>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> list1 = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.CanBeMadeGolden() && x.CardID != "BG29_811")).ToList<Minion>();
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        List<IGrouping<int, Minion>> list2 = list1.GroupBy<Minion, int>((Func<Minion, int>) (x => x.tier)).ToList<IGrouping<int, Minion>>();
        if (!list2.Any<IGrouping<int, Minion>>())
          break;
        Minion minion;
        if (list2.OrderBy<IGrouping<int, Minion>, int>((Func<IGrouping<int, Minion>, int>) (x => x.Key)).First<IGrouping<int, Minion>>().ToList<Minion>().TryGetRandom<Minion>(out minion))
        {
          minion.golden = true;
          list1.Remove(minion);
        }
      }
    });
  }
}
