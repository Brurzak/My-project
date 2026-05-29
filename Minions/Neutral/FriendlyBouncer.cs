// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.FriendlyBouncer
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

public class FriendlyBouncer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_700";
  public const string Text = "<b>Rally:</b> Summon and get a random <b>Rally</b> minion.";
  public const string GoldenText = "<b>Rally:</b> Summon and get 2 random <b>Rally</b> minions.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is FriendlyBouncer))
        return;
      List<Card> list = this.Simulator.MinionFactory.MinionPoolOptionsPlayer(this.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => ((IEnumerable<string>) x.Mechanics).Contains<string>("Rally"))).ToList<Card>();
      List<Summon> summons = new List<Summon>();
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        Card card;
        if (!list.TryGetRandom<Card>(out card))
        {
          this.AddMinionToFriendlyHand();
        }
        else
        {
          this.AddMinionToFriendlyHand(card.Id);
          summons.Add(Summon.FromCard(card));
        }
      }
      minion.TrySummonMinions(summons);
    });
  }
}
