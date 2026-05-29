// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.MasterGadrin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class MasterGadrin(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG20_HERO_201_Buddy";
  public const string Text = "<b>Start of Combat:</b> Give the minion to the left of this Health equal to its Attack.";
  public const string GoldenText = "<b>Start of Combat:</b> Give adjacent minions Health equal to their Attack.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion leftNeighbor = this.GetLeftNeighbor();
      leftNeighbor?.IncreaseStats(0, leftNeighbor.attack());
      if (!this.golden)
        return;
      Minion rightNeighbor = this.GetRightNeighbor();
      rightNeighbor?.IncreaseStats(0, rightNeighbor.attack());
    });
  }
}
