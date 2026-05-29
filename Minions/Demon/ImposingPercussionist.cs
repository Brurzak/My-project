// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.ImposingPercussionist
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
namespace BobsBuddy.Minions.Demon;

public class ImposingPercussionist(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG26_525";
  public const string Text = "<b><b>Battlecry:</b> Discover</b> a Demon. Deal damage to your hero equal to its Tier.";
  public const string GoldenText = "<b><b>Battlecry:</b> Discover</b> 2 Demons. Deal damage to your hero equal to their Tiers.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Card> list = this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 15).Where<Card>((Func<Card, bool>) (x => x.Id != "BG26_525")).ToList<Card>();
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Card card;
        if (!list.TryGetRandom<Card>(out card))
        {
          this.AddMinionToFriendlyHand();
        }
        else
        {
          int techLevel = card.TechLevel;
          this.Simulator.DealDamageToPlayer(this.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent, techLevel, (Entity) this);
          this.AddMinionToFriendlyHand(card.Id);
        }
      }
    });
  }
}
