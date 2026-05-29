// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.CarapaceRaiser
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class CarapaceRaiser(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG33_111";
  public const string Text = "<b>Deathrattle</b>: Get a Haunted Carapace.";
  public const string GoldenText = "<b>Deathrattle</b>: Get 2 Haunted Carapaces.";

  public Action<Minion> GetDeathrattle() => CarapaceRaiser.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      for (int index = 0; index < (minion.golden ? 2 : 1); ++index)
        minion.AddSpellToFriendlyHand();
    });
  }
}
