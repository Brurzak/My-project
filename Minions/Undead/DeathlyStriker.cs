// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.DeathlyStriker
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
namespace BobsBuddy.Minions.Undead;

public class DeathlyStriker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IAvenge
{
  public const string CardId = "BG31_835";
  public const string Text = "<b>Avenge ({0}):</b> Get a random Undead. <b>Deathrattle:</b> Summon it from your hand for this combat only.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get 2 random Undead. <b>Deathrattle:</b> Summon them from your hand for this combat only.";
  private List<Summon> _summons = new List<Summon>();

  public Action<Minion> GetDeathrattle() => DeathlyStriker.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is DeathlyStriker deathlyStriker2))
        return;
      deathlyStriker2.TrySummonMinions(deathlyStriker2._summons);
    });
  }

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      List<Card> list = this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 11);
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
          this._summons.Add(new Summon(card.Id));
        }
      }
    });
  }
}
