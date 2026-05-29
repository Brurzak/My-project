// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.PashmarTheVengeful
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class PashmarTheVengeful(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG23_014";
  public const string Text = "<b>Avenge (3):</b> Get a <b>Spellcraft</b> spell of your Tier or lower.";
  public const string GoldenText = "<b>Avenge (3):</b> Get 2 <b>Spellcraft</b> spells of your Tier or lower.";

  public int AvengeRequirement => 3;

  public Action? OnAvenge()
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
