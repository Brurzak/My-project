// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BarrensBlacksmith
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class BarrensBlacksmith(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BAR_073";
  public const string Text = "<b>Frenzy:</b> Give your other minions +2/+2.";

  public override Action? OnFirstTimeTakenDamage()
  {
    return (Action) (() =>
    {
      if (!this.IsAlive())
        return;
      IEnumerable<Minion> minions = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this));
      int by = this.DoubleIfGolden(2);
      foreach (Minion minion in minions)
        minion.IncreaseStats(by);
    });
  }
}
