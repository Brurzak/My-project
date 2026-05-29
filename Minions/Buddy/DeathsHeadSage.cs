// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.DeathsHeadSage
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class DeathsHeadSage(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG20_HERO_103_Buddy";
  public const string Text = "After you get a <b>Blood Gem</b>, get an extra copy of it.";
  public const string GoldenText = "After you get a <b>Blood Gem</b>, get 2 extra copies of it.";
  public List<CardEntity> _cardsCopied = new List<CardEntity>();

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    return !(card is BloodGem) || source == this || this._cardsCopied.Contains(card.Original) ? (Action) null : (Action) (() =>
    {
      this._cardsCopied.Add(card.Original);
      this.AddCardToFriendlyHand(card.Original.Copy((Entity) this));
      if (!this.golden)
        return;
      this.AddCardToFriendlyHand(card.Original.Copy((Entity) this));
    });
  }
}
