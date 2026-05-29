// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.SaltyHog
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class SaltyHog(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMinionMadeGolden,
  IEntity,
  IOnCardAddedToFriendlyHand
{
  public const string CardId = "BG31_332";
  public const string Text = "Whenever you add 3 cards to your hand, give your other minions +1/+1. <i>(3 left!)</i>";
  public const string GoldenText = "Whenever you add 3 cards to your hand, give your other minions +2/+2. <i>(3 left!)</i>";
  private int _counter;

  public Action? OnMinionMadeGolden() => (Action) (() => this._counter = 0);

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    return (Action) (() =>
    {
      ++this._counter;
      if (this._counter != 3)
        return;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(1), (Entity) this);
      this._counter = 0;
    });
  }
}
