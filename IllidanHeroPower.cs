// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.IllidanHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class IllidanHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnStartOfCombat, IEntity
{
  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count == 0)
        return;
      this.Simulator.state.ActiveSide = (IReadOnlyList<Minion>) this.FriendlySide;
      Minion attacker1 = this.FriendlySide[0];
      Minion attacker2 = this.FriendlySide.Count > 1 ? this.FriendlySide.Last<Minion>() : (Minion) null;
      attacker1.IncreaseStats(2, 1);
      attacker2?.IncreaseStats(2, 1);
      if (attacker1.IsAlive())
      {
        foreach (Trigger trigger in this.Simulator.state.TriggerScope.Data.ToList<Trigger>())
        {
          if (!trigger.Resolved && trigger.Action.Method.Name == "<OnFriendlyMinionBuffed>b__0" && trigger.Source == attacker1)
            trigger.Invoke();
        }
        this.Simulator.AttackWithMinion(attacker1);
      }
      if (attacker2 != null && attacker2.IsAlive())
      {
        foreach (Trigger trigger in this.Simulator.state.TriggerScope.Data.ToList<Trigger>())
        {
          if (!trigger.Resolved && trigger.Action.Method.Name == "<OnFriendlyMinionBuffed>b__0" && trigger.Source == attacker2)
            trigger.Invoke();
        }
        this.Simulator.AttackWithMinion(attacker2);
      }
      this.Simulator.state.ActiveSide = this.ControlledByPlayer ? (IReadOnlyList<Minion>) this.Simulator.opponentSide : (IReadOnlyList<Minion>) this.Simulator.playerSide;
    });
  }
}
