// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.WintergraspGhoul
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class WintergraspGhoul(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_694";
  public const string Text = "<b>Deathrattle:</b> Get a Tomb Turning.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 Tomb Turnings.";

  public Action<Minion> GetDeathrattle() => WintergraspGhoul.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
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
