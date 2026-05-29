// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.ShipInABottle
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class ShipInABottle(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity,
  IOnFriendlyMinionSummoned
{
  public const string CardId = "BG30_MagicItem_407";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Card card;
      if (!this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 23).TryGetRandom<Card>(out card))
        return;
      this.Simulator.TrySummonMinion((Summon) card, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
      this.AddMinionToFriendlyHand(card.Id);
    });
  }

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (source != this)
        return;
      this.Simulator.AttackWithMinion(summoned);
    });
  }
}
