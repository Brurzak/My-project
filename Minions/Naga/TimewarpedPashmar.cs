// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.TimewarpedPashmar
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class TimewarpedPashmar(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_Giant_211";
  public const string Text = "<b>Avenge ({0}):</b> Get a random <b>Spellcraft</b> spell and Tavern spell.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get 2 random <b>Spellcraft</b> spells and Tavern spells.";

  public int AvengeRequirement => 3;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      this.AddSpellToFriendlyHand();
      this.AddSpellToFriendlyHand();
      if (!this.golden)
        return;
      this.AddSpellToFriendlyHand();
      this.AddSpellToFriendlyHand();
    });
  }
}
