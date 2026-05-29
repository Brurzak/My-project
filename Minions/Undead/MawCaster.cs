// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.MawCaster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class MawCaster(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG32_340";
  public const string Text = "<b>Battlecry:</b> Destroy a friendly Undead to <b>Discover</b> an Undead.";
  public const string GoldenText = "<b>Battlecry:</b> Destroy a friendly Undead to <b>Discover</b> 2 Undead.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list1 = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsUndead())).ToList<Minion>();
      Minion target;
      if (!list1.Any<Minion>() || !list1.TryGetRandom<Minion>(out target))
        return;
      this.Simulator.Destroy(target, (Entity) this);
      List<Card> list2 = target.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(target.ControlledByPlayer, (Race) 11);
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        Card card;
        if (!list2.TryGetRandom<Card>(out card))
          this.AddMinionToFriendlyHand();
        else
          this.AddMinionToFriendlyHand(card.Id);
      }
    });
  }
}
