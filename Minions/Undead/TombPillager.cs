// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TombPillager
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TombPillager(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_LOE_012";
  public const string Text = "<b>Deathrattle:</b> Add a Coin to your hand.";

  public Action<Minion> GetDeathrattle() => TombPillager.Deathrattle(this.golden);

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
