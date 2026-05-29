// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Archaedas
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

public class Archaedas(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_651";
  public const string Text = "<b>Battlecry:</b> Get a random Tier 5 minion.";
  public const string GoldenText = "<b>Battlecry:</b> Get two random Tier 5 minions.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Card> list = this.Simulator.MinionFactory.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 5)).ToList<Card>();
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
