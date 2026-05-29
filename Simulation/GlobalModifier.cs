// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.GlobalModifier
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Simulation;

public class GlobalModifier(Simulator simulator, bool controlledByPlayer) : 
  Entity("__GlobalModifier", simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus,
  IOnFriendlyMinionSummoned
{
  public void InitalizeStartingBonusValues(
    int undeadAttackBonus,
    int beastAttackBonus,
    int beastHealthBonus,
    int whelpAttackBonus,
    int whelpHealthBonus,
    int hauntedAtkBuff,
    int hauntedHealthBuff)
  {
    this.UndeadAttackBonus = undeadAttackBonus;
    this.BeastAttackBonus = beastAttackBonus;
    this.BeastHealthBonus = beastHealthBonus;
    this.WhelpAttackBonus = whelpAttackBonus;
    this.WhelpHealthBonus = whelpHealthBonus;
    if (this.ControlledByPlayer)
      return;
    this.AttackBonus = hauntedAtkBuff;
    this.HealthBonus = hauntedHealthBuff;
  }

  public int PirateSummonAttackBonus { get; set; }

  public int AttackBonus { get; private set; }

  public int HealthBonus { get; private set; }

  public int UndeadAttackBonus { get; private set; }

  public int BeastAttackBonus { get; private set; }

  public int BeastHealthBonus { get; private set; }

  public int OddTierAttackBonus { get; private set; }

  public int OddTierHealthBonus { get; private set; }

  public int WhelpAttackBonus { get; private set; }

  public int WhelpHealthBonus { get; private set; }

  public void IncreaseAttackBonus(int atkBuff, Entity? source)
  {
    this.AttackBonus += atkBuff;
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
      {
        foreach (Minion buffed in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
          this.Simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(buffed, atkBuff, 0, source), (IEntity) source);
      }
    }
  }

  public void IncreaseUndeadAttackBonus(int atkBuff, Entity? source)
  {
    this.UndeadAttackBonus += atkBuff;
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
      {
        foreach (Minion buffed in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsUndead() && x.IsAlive())))
          this.Simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(buffed, atkBuff, 0, source), (IEntity) source);
      }
    }
  }

  public void IncreaseBeastBonus(int atkBuff, int healthBuff, Entity? source)
  {
    this.BeastAttackBonus += atkBuff;
    this.BeastHealthBonus += healthBuff;
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
      {
        foreach (Minion buffed in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsBeast() && x.IsAlive())))
          this.Simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(buffed, atkBuff, healthBuff, source), (IEntity) source);
      }
    }
  }

  public void IncreaseOddTierBonus(int atkBuff, int healthBuff, Entity? source)
  {
    this.OddTierAttackBonus += atkBuff;
    this.OddTierHealthBonus += healthBuff;
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
      {
        foreach (Minion buffed in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.tier % 2 != 0 && x.IsAlive())))
          this.Simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(buffed, atkBuff, healthBuff, source), (IEntity) source);
      }
    }
  }

  public void IncreaseWhelpBonus(int atkBuff, int healthBuff, Entity? source)
  {
    this.WhelpAttackBonus += atkBuff;
    this.WhelpHealthBonus += healthBuff;
    foreach (Entity friendlyEntity in this.FriendlyEntities)
    {
      if (friendlyEntity is IOnFriendlyMinionBuffed friendlyMinionBuffed)
      {
        foreach (Minion buffed in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsWhelp && x.IsAlive())))
          this.Simulator.RegisterTrigger(friendlyMinionBuffed.OnFriendlyMinionBuffed(buffed, atkBuff, healthBuff, source), (IEntity) source);
      }
    }
  }

  public int PassiveAttackBonusFor(Minion target)
  {
    int attackBonus = this.AttackBonus;
    if (target.IsUndead())
      attackBonus += this.UndeadAttackBonus;
    if (target.IsBeast())
      attackBonus += this.BeastAttackBonus;
    if (target.tier % 2 != 0)
      attackBonus += this.OddTierAttackBonus;
    if (target.IsWhelp)
      attackBonus += this.WhelpAttackBonus;
    return attackBonus;
  }

  public int PassiveHealthBonusFor(Minion target)
  {
    int healthBonus = this.HealthBonus;
    if (target.IsBeast())
      healthBonus += this.BeastHealthBonus;
    if (target.tier % 2 != 0)
      healthBonus += this.OddTierHealthBonus;
    if (target.IsWhelp)
      healthBonus += this.WhelpHealthBonus;
    return healthBonus;
  }

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsPirate() || this.PirateSummonAttackBonus <= 0)
        return;
      summoned.IncreaseStats(this.PirateSummonAttackBonus, 0);
    });
  }
}
