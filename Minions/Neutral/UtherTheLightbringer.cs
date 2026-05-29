// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.UtherTheLightbringer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class UtherTheLightbringer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG23_190";
  public const string Text = "<b>Battlecry:</b> Set a minion's Attack and Health to 15.";
  public const string GoldenText = "<b>Battlecry:</b> Set a minion's Attack and Health to 30.";

  public Action? OnBattlecry()
  {
    List<Minion> bothSides = this.FriendlySide.Concat<Minion>((IEnumerable<Minion>) this.OpposingSide).ToList<Minion>();
    return bothSides.Count == 0 ? (Action) null : (Action) (() =>
    {
      int num = this.DoubleIfGolden(15);
      bothSides.GetRandom<Minion>().SetStats(new int?(num), new int?(num));
    });
  }
}
