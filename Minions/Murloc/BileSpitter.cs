// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.BileSpitter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class BileSpitter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_318";
  public const string Text = "<b>Venomous</b> <b>Rally:</b> Give another friendly Murloc <b>Venomous</b>.";
  public const string GoldenText = "<b>Venomous</b> <b>Rally:</b> Give 2 other friendly Murlocs <b>Venomous</b>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion1;
        if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != minion && !x.venomous && x.IsMurloc())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
          minion1.venomous = true;
      }
    });
  }
}
