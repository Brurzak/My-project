// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BarrensBrawler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class BarrensBrawler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG29_861";
  public const string Text = "<b>Battlecry:</b> Get a random <b>Deathrattle</b> minion.";
  public const string GoldenText = "<b>Battlecry:</b> Get 2 random <b>Deathrattle</b> minions.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Card> list = this.Simulator.MinionFactory.MinionPoolOptionsPlayer(this.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Deathrattle)).ToList<Card>();
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
