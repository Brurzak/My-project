// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.MysticSporebat
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class MysticSporebat(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG28_900";
  public const string Text = "<b>Deathrattle:</b> Get a random Tavern spell.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 random Tavern spells.";

  public Action<Minion> GetDeathrattle() => MysticSporebat.Deathrattle(this.golden);

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
