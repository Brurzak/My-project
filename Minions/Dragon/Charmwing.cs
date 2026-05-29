// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Charmwing
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

public class Charmwing(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_240";
  public const string Text = "<b>Rally:</b> Give {0} friendly Dragons this minion's Health <i>(except Charmwing)</i>.";
  public const string GoldenText = "<b>Rally:</b> Give {0} friendly Dragons this minion's Health, twice <i>(except Charmwing)</i>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> randomElements = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.CardID != minion.CardID && x.IsDragon() && x.IsAlive())).ToList<Minion>().GetRandomElements<Minion>(2);
      int num = isGolden ? 2 : 1;
      foreach (Minion minion1 in randomElements)
      {
        for (int index = 0; index < num; ++index)
          minion1.IncreaseStats(0, minion.health());
      }
    });
  }
}
