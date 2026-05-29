// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.Thorncaptain
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class Thorncaptain(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG25_045";
  public const string Text = "Whenever a card is added to your hand, gain +1 Health until next turn.";
  public const string GoldenText = "Whenever a card is added to your hand, gain +2 Health until next turn.";

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    return (Action) (() => this.IncreaseStats(0, this.golden ? 2 : 1, (Entity) this));
  }
}
