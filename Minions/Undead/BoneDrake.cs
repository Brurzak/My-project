// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.BoneDrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class BoneDrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_ICC_027";
  public const string Text = "<b>Deathrattle:</b> Add a random Dragon to your hand.";

  public Action<Minion> GetDeathrattle() => BoneDrake.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Card card;
      if (!minion.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(minion.ControlledByPlayer, (Race) 24).TryGetRandom<Card>(out card))
        minion.AddMinionToFriendlyHand();
      else
        minion.AddMinionToFriendlyHand(card.Id);
    });
  }
}
