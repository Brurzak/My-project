// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.AudaciousAnchor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class AudaciousAnchor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG28_904";
  public const string Text = "<b>Start of Combat:</b> Battle the nearest enemy minion to the death!";
  public const string GoldenText = "<b>Start of Combat:</b> Battle the nearest enemy minion to the death. Then battle again!";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        if (!this.IsAlive())
          break;
        IEnumerable<Minion> source = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive()));
        if (!source.Any<Minion>())
          break;
        double targetIndex = Targeting.GetOppositeIndex((Minion) this);
        this.BattleToDeath(source.GroupBy<Minion, double>((Func<Minion, double>) (x => Math.Abs((double) x.BoardPosition() - targetIndex))).OrderBy<IGrouping<double, Minion>, double>((Func<IGrouping<double, Minion>, double>) (x => x.Key)).First<IGrouping<double, Minion>>().ToList<Minion>().GetRandom<Minion>());
      }
    });
  }

  private void BattleToDeath(Minion target)
  {
    Minion attacker = (Minion) this;
    while (this.IsAlive() && target.IsAlive())
    {
      Minion target1 = attacker == this ? target : (Minion) this;
      this.Simulator.AttackWithMinion(attacker, target1, true);
      attacker = target1;
    }
  }
}
