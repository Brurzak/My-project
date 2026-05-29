// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.HandOfDeios
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells;

public class HandOfDeios(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_991";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion minion1;
      if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.HasDeathrattle() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
      {
        this.Simulator.TriggerDeathrattles(minion1);
        this.Simulator.ResolveTriggersInCurrentScope();
      }
      Minion target;
      if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.GetTriggers<IBattlecry>().Any<IBattlecry>() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target))
        this.Simulator.InvokeBattlecry(target);
      Minion minion2;
      if (!this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.GetTriggers<IOnRally>().Any<IOnRally>() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion2))
        return;
      this.Simulator.TriggerRally(minion2);
    });
  }
}
