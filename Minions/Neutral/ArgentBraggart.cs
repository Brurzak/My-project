// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.ArgentBraggart
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class ArgentBraggart(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG_SCH_149";
  public const string Text = "<b>Battlecry:</b> Set this minion's Attack and Health to the highest in the battlefield.";
  public const string GoldenText = "<b>Battlecry:</b> Set this minion's Attack and Health to double the highest in the battlefield.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      IEnumerable<Minion> source = this.Simulator.playerSide.Concat<Minion>((IEnumerable<Minion>) this.Simulator.opponentSide);
      int i1 = source.Select<Minion, int>((Func<Minion, int>) (m => m.attack())).Max();
      int i2 = source.Select<Minion, int>((Func<Minion, int>) (m => m.health())).Max();
      this.SetStats(new int?(this.DoubleIfGolden(i1)), new int?(this.DoubleIfGolden(i2)));
    });
  }
}
