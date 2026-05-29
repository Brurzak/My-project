// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TimewarpedHag
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TimewarpedHag(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_342";
  public const string Text = "<b>Start of Combat:</b> Give the Undead to the right <b>Reborn</b> and \"This is <b>Reborn</b> with full Health and enchantments\".";
  public const string GoldenText = "<b>Start of Combat:</b> Give adjacent Undead <b>Reborn</b> and \"This is <b>Reborn</b> with full Health and enchantments\".";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> rightNeighbors = this.GetRightNeighbors(1);
      if (this.golden)
        rightNeighbors.AddRange((IEnumerable<Minion>) this.GetLeftNeighbors(1));
      foreach (Minion minion in rightNeighbors)
      {
        if (minion.IsUndead())
        {
          minion.reborn = true;
          minion.TryAttachEnchantment("BACON_FAKE_TimewarpedHagEnchantment");
        }
      }
    });
  }
}
