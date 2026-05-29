// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.DrustfallenButcher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class DrustfallenButcher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG32_324";
  public const string Text = "<b>Avenge ({0}):</b> Get a Butchering.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get 2 Butcherings.";

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(1);
      for (int index = 0; index < num; ++index)
        this.AddSpellToFriendlyHand();
    });
  }
}
