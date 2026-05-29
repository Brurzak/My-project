// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.RaptorElder
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class RaptorElder(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus
{
  public const string CardId = "BG33_842";
  public const string Text = "<b>Stealth</b>. Your Beasts have +{0}/+{1}. <i>(Improved by each Beast you've summoned this combat!)</i>";
  public const string GoldenText = "<b>Stealth</b>. Your Beasts have +{0}/+{1}. <i>(Improved by each Beast you've summoned this combat!)</i>";

  private List<Minion> SummonedMinions
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.SummonedMinions : this.Simulator.state.Player.SummonedMinions;
    }
  }

  private int CombatCounter
  {
    get => this.SummonedMinions.Count<Minion>((Func<Minion, bool>) (x => x.IsBeast()));
  }

  public int PassiveAttackBonusFor(Minion target)
  {
    return !target.IsBeast() ? 0 : this.DoubleIfGolden(3) * (this.CombatCounter + 1);
  }

  public int PassiveHealthBonusFor(Minion target)
  {
    return !target.IsBeast() ? 0 : this.DoubleIfGolden(2) * (this.CombatCounter + 1);
  }
}
