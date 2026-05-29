// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.TimewarpedGuard
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class TimewarpedGuard(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_Giant_068";
  public const string Text = "<b>Divine Shield</b> <b>Rally:</b> Give a different friendly minion <b>Divine Shield</b> permanently.";
  public const string GoldenText = "<b>Divine Shield</b> <b>Rally:</b> Give 2 different friendly minions <b>Divine Shield</b> permanently.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion1;
        if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.CardID != minion.CardID && !x.hasDiv)).ToList<Minion>().TryGetRandom<Minion>(out minion1))
          minion1.div = 1;
      }
    });
  }
}
