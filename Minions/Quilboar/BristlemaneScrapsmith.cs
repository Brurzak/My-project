// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.BristlemaneScrapsmith
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class BristlemaneScrapsmith(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG24_707";
  public const string Text = "After a friendly minion with <b>Taunt</b> dies, get a <b>Blood Gem</b>.";
  public const string GoldenText = "After a friendly minion with <b>Taunt</b> dies, get 2 <b>Blood Gems</b>.";

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.taunt)
        return;
      this.AddBloodGemToFriendlyHand();
      if (!this.golden)
        return;
      this.AddBloodGemToFriendlyHand();
    });
  }
}
