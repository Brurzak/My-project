// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.SaltyLooter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class SaltyLooter(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BGS_081";
  public const string Text = "Has +1/+1 for each Pirate you've summoned this game <i>(wherever this is)</i>.";
  public const string GoldenText = "Has +2/+2 for each Pirate you've summoned this game <i>(wherever this is)</i>.";

  public override int SelfAttackPassive()
  {
    return this.DoubleIfGolden(this.GlobalCounter + this.CombatCounter);
  }

  public override int SelfHealthPassive()
  {
    return this.DoubleIfGolden(this.GlobalCounter + this.CombatCounter);
  }

  private int GlobalCounter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.PiratesSummonCounter : this.Simulator.state.Player.PiratesSummonCounter;
    }
  }

  private int CombatCounter
  {
    get => this.SummonedMinions.Count<Minion>((Func<Minion, bool>) (x => x.IsPirate()));
  }

  private List<Minion> SummonedMinions
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.SummonedMinions : this.Simulator.state.Player.SummonedMinions;
    }
  }
}
