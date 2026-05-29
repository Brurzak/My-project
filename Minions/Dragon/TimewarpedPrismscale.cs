// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TimewarpedPrismscale
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TimewarpedPrismscale(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_PreMadeChamp_022";
  public const string Text = "<b>Avenge ({0}):</b> Get an Azerite Empowerment.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get 2 Azerite Empowerments.";

  public int AvengeRequirement => 2;

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
