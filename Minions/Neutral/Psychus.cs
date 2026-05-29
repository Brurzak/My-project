// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Psychus
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class Psychus(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_318";
  public const string Text = "<b>Start of Combat:</b> Set this minion's Attack and Health to match the highest in the battlefield.";
  public const string GoldenText = "<b>Start of Combat:</b> Set this minion's Attack and Health to match double the highest in the battlefield.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion[] array = this.FriendlySide.Concat<Minion>((IEnumerable<Minion>) this.OpposingSide).ToArray<Minion>();
      int i1 = ((IEnumerable<Minion>) array).Max<Minion>((Func<Minion, int>) (m => m.attack()));
      int i2 = ((IEnumerable<Minion>) array).Max<Minion>((Func<Minion, int>) (m => m.health()));
      this.SetStats(new int?(this.DoubleIfGolden(i1)), new int?(this.DoubleIfGolden(i2)));
    });
  }
}
