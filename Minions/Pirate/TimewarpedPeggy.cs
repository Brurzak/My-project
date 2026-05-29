// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.TimewarpedPeggy
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class TimewarpedPeggy(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG34_Giant_327";
  public const string Text = "Whenever a card is added to your hand, give your Pirates +{0}/+{1}.";
  public const string GoldenText = "Whenever a card is added to your hand, give your Pirates +{0}/+{1} twice.";

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsPirate() && x.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(1), (Entity) this);
    });
  }
}
