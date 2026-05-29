// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Quests.Rewards.RighteousCharge
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Quests.Rewards;

public class RighteousCharge(string cardId, Simulator simulator, bool controlledByPlayer) : 
  QuestReward(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG33_Reward_003";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      this.Simulator.state.ActiveSide = (IReadOnlyList<Minion>) this.FriendlySide;
      if (this.FriendlySide.Count == 0)
        return;
      Minion attacker = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().FirstOrDefault<Minion>();
      if (attacker == null)
        return;
      attacker.div = 1;
      if (attacker.IsAlive())
        this.Simulator.AttackWithMinion(attacker);
      this.Simulator.state.ActiveSide = this.ControlledByPlayer ? (IReadOnlyList<Minion>) this.Simulator.opponentSide : (IReadOnlyList<Minion>) this.Simulator.playerSide;
    });
  }
}
