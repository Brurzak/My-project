// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.OozelingGladiator
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class OozelingGladiator(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG27_002";
  public const string Text = "<b>Battlecry:</b> Get two Slimy Shields that give +1/+1 and <b><b>Taunt</b>.</b>";
  public const string GoldenText = "<b>Battlecry:</b> Get four Slimy Shields that give +1/+1 and <b><b>Taunt</b>.</b>";

  public Action? OnBattlecry()
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
