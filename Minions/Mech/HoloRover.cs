// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.HoloRover
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
namespace BobsBuddy.Minions.Mech;

public class HoloRover(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG31_175";
  public const string Text = "<b><b>Divine Shield</b> </b><b>Rally:</b> Get a random <b>Magnetic</b> Mech.";
  public const string GoldenText = "<b><b>Divine Shield</b> </b><b>Rally:</b> Get 2 random <b>Magnetic</b> Mechs.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      List<Card> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayer(minion.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Entity.GetTag((GameTag) 849) > 0)).ToList<Card>();
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
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
