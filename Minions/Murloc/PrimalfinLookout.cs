// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.PrimalfinLookout
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
namespace BobsBuddy.Minions.Murloc;

public class PrimalfinLookout(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGS_020";
  public const string Text = "<b>Battlecry:</b> If you control another Murloc, <b>Discover</b> a Murloc.";
  public const string GoldenText = "<b>Battlecry:</b> If you control another Murloc, <b>Discover</b> 2 Murlocs.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      if (!this.FriendlySide.Any<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsMurloc() && x != this)))
        return;
      List<Card> list = this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 14);
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        Card card;
        if (!list.TryGetRandom<Card>(out card))
          this.AddMinionToFriendlyHand();
        else
          this.AddMinionToFriendlyHand(card.Id);
      }
    });
  }
}
