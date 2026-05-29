// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.BellowingTyrant
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class BellowingTyrant(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG31_361";
  public const string Text = "<b>Taunt</b> Has +4/+3 for each Beast you've summoned this game <i>(wherever this is)</i>.";
  public const string GoldenText = "<b>Taunt</b> Has +8/+6 for each Beast you've summoned this game <i>(wherever this is)</i>.";

  public override int SelfAttackPassive()
  {
    return this.DoubleIfGolden(4) * (this.GlobalCounter + this.CombatCounter);
  }

  public override int SelfHealthPassive()
  {
    return this.DoubleIfGolden(3) * (this.GlobalCounter + this.CombatCounter);
  }

  private int GlobalCounter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.BeastsSummonCounter : this.Simulator.state.Player.BeastsSummonCounter;
    }
  }

  private int CombatCounter
  {
    get => this.SummonedMinions.Count<Minion>((Func<Minion, bool>) (x => x.IsBeast()));
  }

  private List<Minion> SummonedMinions
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.SummonedMinions : this.Simulator.state.Player.SummonedMinions;
    }
  }
}
