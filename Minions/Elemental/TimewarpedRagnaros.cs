// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.TimewarpedRagnaros
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class TimewarpedRagnaros(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_580";
  public const string Text = "<b>Start of Combat:</b> Deal this minion's Attack to the highest-Health enemy minion.";
  public const string GoldenText = "<b>Start of Combat:</b> Deal double this minion's Attack to the highest-Health enemy minion.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (!this.OpposingSide.Any<Minion>())
        return;
      int amount = this.DoubleIfGolden(this.attack());
      int highestHealth = this.OpposingSide.Max<Minion>((Func<Minion, int>) (m => m.health()));
      Minion target;
      if (!this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.health() == highestHealth)).ToList<Minion>().TryGetRandom<Minion>(out target))
        return;
      this.Simulator.ProcessDamage(amount, target, (Entity) this);
    });
  }
}
