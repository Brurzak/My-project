// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.SpiritedWhimsydrake
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
namespace BobsBuddy.Minions.Dragon;

public class SpiritedWhimsydrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG32_823";
  public const string Text = "<b>Battlecry:</b> Get a random Dragon.";
  public const string GoldenText = "<b>Battlecry:</b> Get 2 random Dragons.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Card> list = this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 24);
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
