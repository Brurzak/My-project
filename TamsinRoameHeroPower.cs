// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.TamsinRoameHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class TamsinRoameHeroPower(
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
    return (Action) (() => this.Simulator.GetLowestAttackMinion(this.FriendlySide)?.AdditionalDeathrattles.Add(TamsinRoameHeroPower.AdditionalDeathrattle()));
  }

  private static Action<Minion> AdditionalDeathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      int atkBuff = minion.attack();
      int healthBuff = minion.maxHealth;
      minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != minion && x.IsAlive())).ToList<Minion>().ForEach((Action<Minion>) (m => m.IncreaseStats(atkBuff, healthBuff)));
    });
  }
}
