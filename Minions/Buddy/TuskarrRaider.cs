// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.TuskarrRaider
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class TuskarrRaider(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle,
  IOnRally
{
  public const string CardId = "TB_BaconShop_HERO_18_Buddy";
  public const string Text = "<b>Battlecry, Deathrattle, and Rally:</b> Get a random <b>Bounty</b>.";
  public const string GoldenText = "<b>Battlecry, Deathrattle, and Rally:</b> Get 2 random <b>Bounties</b>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.AddSpellToFriendlyHand();
      if (!this.golden)
        return;
      this.AddSpellToFriendlyHand();
    });
  }

  public Action<Minion>? GetDeathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      this.AddSpellToFriendlyHand();
      if (!this.golden)
        return;
      this.AddSpellToFriendlyHand();
    });
  }

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddSpellToFriendlyHand();
      if (!isGolden)
        return;
      minion.AddSpellToFriendlyHand();
    });
  }
}
