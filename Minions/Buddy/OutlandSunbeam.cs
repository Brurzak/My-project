// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.OutlandSunbeam
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class OutlandSunbeam(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_HERO_003_Buddy";
  public const string Text = "<b>Battlecry:</b> <b>Discover</b> a Tavern spell that costs (3) or more from any Tier.";
  public const string GoldenText = "<b>Battlecry:</b> <b>Discover</b> two Tavern spells that cost (3) or more from any Tier.";

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
}
