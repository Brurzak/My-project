// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedSporebat
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedSporebat(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_582";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Get a random Tavern spell that costs (2) or more.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Get 2 random Tavern spells that cost (2) or more.";

  public Action<Minion> GetDeathrattle() => TimewarpedSporebat.Deathrattle(this.golden);

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
