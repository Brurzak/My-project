// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.GormlingGourmet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class GormlingGourmet(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG32_336";
  public const string Text = "<b>Taunt</b> <b>Battlecry and Deathrattle:</b> Get a Seafood Stew.";
  public const string GoldenText = "<b>Taunt</b> <b>Battlecry and Deathrattle:</b> Get 2 Seafood Stews.";

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

  public Action<Minion> GetDeathrattle() => GormlingGourmet.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddSpellToFriendlyHand();
      if (!golden)
        return;
      minion.AddSpellToFriendlyHand();
    });
  }
}
