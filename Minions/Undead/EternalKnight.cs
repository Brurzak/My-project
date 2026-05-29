// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.EternalKnight
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class EternalKnight(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG25_008";
  public const string Text = "Has +{0}/+{1} for each friendly Eternal Knight that died this game <i>(wherever this is)</i>.";
  public const string GoldenText = "Has +{0}/+{1} for each friendly Eternal Knight that died this game <i>(wherever this is)</i>.";

  public override int SelfAttackPassive() => this.StatBonus * 4;

  public override int SelfHealthPassive() => this.StatBonus;

  private int StatBonus
  {
    get
    {
      return (this.golden ? 2 : 1) * (this.Counter + this.Graveyard.Count<Minion>((Func<Minion, bool>) (x => x.CardID == "BG25_008")));
    }
  }

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.EternalKnightCounter : this.Simulator.state.Player.EternalKnightCounter;
    }
  }

  private List<Minion> Graveyard
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.DeadMinions : this.Simulator.state.Player.DeadMinions;
    }
  }
}
