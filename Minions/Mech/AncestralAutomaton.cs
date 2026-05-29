// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.AncestralAutomaton
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class AncestralAutomaton(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnSummoned,
  IEntity
{
  public const string CardId = "BG_TTN_401";
  public const string Text = "Has +3/+2 for each other Ancestral Automaton you've summoned this game <i>(wherever this is)</i>.";
  public const string GoldenText = "Has +6/+4 for each other Ancestral Automaton you've summoned this game <i>(wherever this is)</i>.";

  public Action? OnSummoned()
  {
    return (Action) (() =>
    {
      if (this.ControlledByPlayer)
        ++this.Simulator.state.Player.AncestralAutomatonCounter;
      else
        ++this.Simulator.state.Opponent.AncestralAutomatonCounter;
    });
  }

  public override int SelfAttackPassive() => this.DoubleIfGolden(3) * Math.Max(this.Counter - 1, 0);

  public override int SelfHealthPassive() => this.DoubleIfGolden(2) * Math.Max(this.Counter - 1, 0);

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.AncestralAutomatonCounter : this.Simulator.state.Player.AncestralAutomatonCounter;
    }
  }
}
