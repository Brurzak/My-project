// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.HighkeeperRa
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

public class HighkeeperRa(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity,
  IDeathrattle,
  IBattlecry
{
  public const string CardId = "BG34_319";
  public const string Text = "<b>Battlecry, Deathrattle, and Rally:</b> Get a random Tier 6 minion.";
  public const string GoldenText = "<b>Battlecry, Deathrattle, and Rally:</b> Get two random Tier 6 minions.";

  public Action<Minion> OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      List<Card> list = minion.Simulator.MinionFactory.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 6)).ToList<Card>();
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Card card;
        if (!list.TryGetRandom<Card>(out card))
          this.AddMinionToFriendlyHand();
        else
          this.AddMinionToFriendlyHand(card.Id);
      }
    });
  }

  public Action<Minion> GetDeathrattle() => this.Deathrattle(this.golden);

  public Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Card> list = this.Simulator.MinionFactory.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 6)).ToList<Card>();
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Card card;
        if (!list.TryGetRandom<Card>(out card))
          this.AddMinionToFriendlyHand();
        else
          this.AddMinionToFriendlyHand(card.Id);
      }
    });
  }

  public Action OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Card> list = this.Simulator.MinionFactory.BaconPoolMinionsFilteredLobbyRaces.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 6)).ToList<Card>();
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
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
