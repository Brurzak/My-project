// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.LeylineSurfacer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class LeylineSurfacer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG35_881";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Get an Arcane Absorption.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Get 2 Arcane Absorptions.";

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

  public Action<Minion> GetDeathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddSpellToFriendlyHand();
      if (!minion.golden)
        return;
      minion.AddSpellToFriendlyHand();
    });
  }
}
