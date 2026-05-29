// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BarrensConjurer
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
namespace BobsBuddy.Minions.Neutral;

public class BarrensConjurer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG29_862";
  public const string Text = "<b>Deathrattle:</b> Get a random <b>Battlecry</b> minion.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 random <b>Battlecry</b> minions.";

  public Action<Minion> GetDeathrattle() => BarrensConjurer.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Card> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayer(minion.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Entity.GetTag((GameTag) 218) > 0)).ToList<Card>();
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Card card;
        if (!list.TryGetRandom<Card>(out card))
          minion.AddMinionToFriendlyHand();
        else
          minion.AddMinionToFriendlyHand(card.Id);
      }
    });
  }
}
