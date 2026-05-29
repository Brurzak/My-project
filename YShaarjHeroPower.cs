// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.YShaarjHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class YShaarjHeroPower(
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
    return !this.Data.IsActivated ? (Action) null : (Action) (() =>
    {
      int friendlyTier = this.ControlledByPlayer ? this.Simulator.state.Player.Tier : this.Simulator.state.Opponent.Tier;
      Card card;
      if (!this.Simulator.MinionFactory.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.TechLevel == friendlyTier)).ToList<Card>().TryGetRandom<Card>(out card))
      {
        this.AddMinionToFriendlyHand();
      }
      else
      {
        this.Simulator.TrySummonMinion((Summon) card, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
        this.AddMinionToFriendlyHand(card.Id);
      }
    });
  }
}
