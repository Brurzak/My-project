// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.PeggySturdybone
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class PeggySturdybone(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG25_032";
  public const string Text = "Whenever a card is added to your hand, give another friendly Pirate +{0}/+{1}.";
  public const string GoldenText = "Whenever a card is added to your hand, give another friendly Pirate +{0}/+{1}, twice.";

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion;
        if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsPirate() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion))
          minion.IncreaseStats(2, 1, (Entity) this);
      }
    });
  }
}
