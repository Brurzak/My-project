// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.TimewarpedCalligrapher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class TimewarpedCalligrapher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IBattlecry,
  IOnRally
{
  public const string CardId = "BG34_PreMadeChamp_091";
  public const string Text = "<b>Battlecry, Deathrattle, and Rally:</b> Get a random Tavern spell.";
  public const string GoldenText = "<b>Battlecry, Deathrattle, and Rally:</b> Get 2 random Tavern spells.";

  public Action<Minion> GetDeathrattle() => TimewarpedCalligrapher.Deathrattle(this.golden);

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

  public Action OnBattlecry() => (Action) (() => this.GetDeathrattle()((Minion) this));

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion => this.GetDeathrattle()(minion));
  }
}
