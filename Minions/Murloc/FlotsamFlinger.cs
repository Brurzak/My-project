// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.FlotsamFlinger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class FlotsamFlinger(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG33_892";
  public const string Text = "At the start of your turn, trigger the <b>Battlecries</b> of your minions from Tier 3 or below.";
  public const string GoldenText = "At the start of your turn, trigger the <b>Battlecries</b> of your minions from Tier 3 or below twice.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.GetTriggers<IBattlecry>().Any<IBattlecry>() && x.tier >= 3)).ToList<Minion>();
        if (!list.Any<Minion>())
          break;
        foreach (Minion target in list)
          this.Simulator.InvokeBattlecry(target);
      }
    });
  }
}
