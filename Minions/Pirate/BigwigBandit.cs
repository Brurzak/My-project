// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.BigwigBandit
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class BigwigBandit(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_822";
  public const string Text = "<b>Rally:</b> Get a random <b>Bounty</b>.";
  public const string GoldenText = "<b>Rally:</b> Get 2 random <b>Bounties</b>.";

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
