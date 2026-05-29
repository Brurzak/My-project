// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.BrinyBootlegger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class BrinyBootlegger(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG21_017";
  public const string Text = "<b>Deathrattle:</b> Get a Tavern Coin.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 Tavern Coins.";

  public Action<Minion> GetDeathrattle() => BrinyBootlegger.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddSpellToFriendlyHand();
      if (!golden)
        return;
      minion.AddSpellToFriendlyHand();
    });
  }
}
