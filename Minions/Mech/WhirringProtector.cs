// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.WhirringProtector
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class WhirringProtector(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_807";
  public const string Text = "<b>Magnetic</b> <b>Rally:</b> Give your other minions +{1} Attack.";
  public const string GoldenText = "<b>Magnetic</b> <b>Rally:</b> Give your other minions +{1} Attack.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion => WhirringProtector.Rally(this.golden)(minion));
  }

  public static Action<Minion> Rally(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      IEnumerable<Minion> minions = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != minion));
      int attackBuff = golden ? 10 : 5;
      foreach (Minion minion1 in minions)
        minion1.IncreaseStats(attackBuff, 0);
    });
  }
}
