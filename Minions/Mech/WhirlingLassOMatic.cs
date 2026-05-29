// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.WhirlingLassOMatic
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class WhirlingLassOMatic(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG28_635";
  public const string Text = "<b>Divine Shield</b>, <b>Windfury</b> <b>Rally:</b> Get a random Tavern spell.";
  public const string GoldenText = "<b>Divine Shield</b>, <b>Windfury</b> <b>Rally:</b> Get 2 random Tavern spells.";

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
