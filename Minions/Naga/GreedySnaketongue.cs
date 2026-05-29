// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.GreedySnaketongue
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class GreedySnaketongue(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_315";
  public const string Text = "<b>Rally:</b> Get a Tavern Coin.";
  public const string GoldenText = "<b>Rally:</b> Get 2 Tavern Coins.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 2 : 1;
      for (int index = 0; index < num; ++index)
        minion.AddSpellToFriendlyHand();
    });
  }
}
