// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.TimewarpedKilrek
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class TimewarpedKilrek(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_584";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Get a random Demon.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Get 2 random Demons.";

  public Action<Minion> GetDeathrattle() => TimewarpedKilrek.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Card> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(minion.ControlledByPlayer, (Race) 15);
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
