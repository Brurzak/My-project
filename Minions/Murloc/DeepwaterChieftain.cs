// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.DeepwaterChieftain
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class DeepwaterChieftain(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG35_143";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Get a Deepwater Clan.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Get 2 Deepwater Clans.";

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
      this.AddSpellToFriendlyHand();
      if (!minion.golden)
        return;
      this.AddSpellToFriendlyHand();
    });
  }
}
