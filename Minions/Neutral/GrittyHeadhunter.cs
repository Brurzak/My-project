// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.GrittyHeadhunter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class GrittyHeadhunter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_822";
  public const string Text = "<b>Battlecry:</b> Get a Marauder's Contract.";
  public const string GoldenText = "<b>Battlecry:</b> Get 2 Marauder's Contracts.";

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
