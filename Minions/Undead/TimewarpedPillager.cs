// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TimewarpedPillager
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TimewarpedPillager(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_204";
  public const string Text = "<b>Taunt</b>, <b>Reborn</b> <b>Deathrattle:</b> Get a Tavern Coin.";
  public const string GoldenText = "<b>Taunt</b>, <b>Reborn</b> <b>Deathrattle:</b> Get 2 Tavern Coins.";

  public Action<Minion> GetDeathrattle() => TimewarpedPillager.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddSpellToFriendlyHand();
      if (!minion.golden)
        return;
      minion.AddSpellToFriendlyHand();
    });
  }
}
