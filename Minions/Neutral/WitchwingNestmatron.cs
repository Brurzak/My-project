// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.WitchwingNestmatron
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

public class WitchwingNestmatron(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG21_038";
  public const string Text = "<b>Avenge ({0}):</b> Get a random <b>Battlecry</b> minion.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get 2 random <b>Battlecry</b> minions.";

  public int AvengeRequirement => 3;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      List<Card> list = this.Simulator.MinionFactory.MinionPoolOptionsPlayer(this.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Entity.GetTag((GameTag) 218) > 0)).ToList<Card>();
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
