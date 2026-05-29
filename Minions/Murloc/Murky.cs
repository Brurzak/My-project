// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.Murky
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class Murky(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG24_012";
  public const string Text = "<b>Battlecry:</b> Give a friendly Murloc +{0}/+{1} for each one you control.";
  public const string GoldenText = "<b>Battlecry:</b> Give a friendly Murloc +{0}/+{1} for each one you control.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsMurloc() && x.IsAlive())).ToList<Minion>();
      int by = this.DoubleIfGolden(4 * list.Count);
      Minion minion;
      if (!list.TryGetRandom<Minion>(out minion))
        return;
      minion.IncreaseStats(by);
    });
  }
}
