// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Duos.SanlaynScribe
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Duos;

public class SanlaynScribe(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BGDUO31_208";
  public const string Text = "Has +4/+4 for each of your team's San'layn Scribes that died this game <i>(wherever this is)</i>.";
  public const string GoldenText = "Has +8/+8 for each of your team's San'layn Scribes that died this game <i>(wherever this is)</i>.";

  public override int SelfAttackPassive() => this.StatBonus;

  public override int SelfHealthPassive() => this.StatBonus;

  private int StatBonus
  {
    get
    {
      return this.DoubleIfGolden(4) * (this.Counter + this.Graveyard.Count<Minion>((Func<Minion, bool>) (x => x.CardID == "BGDUO31_208")));
    }
  }

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.SanlaynScribeCounter : this.Simulator.state.Player.SanlaynScribeCounter;
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
