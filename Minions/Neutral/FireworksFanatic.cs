// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.FireworksFanatic
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class FireworksFanatic(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG25_922";
  public const string Text = "Whenever you get a minion you already have, give your minions +2/+2.";
  public const string GoldenText = "Whenever you get a minion you already have, give your minions +4/+4.";

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    MinionCardEntity m = card as MinionCardEntity;
    if (m == null)
      return (Action) null;
    return this.FriendlySide.All<Minion>((Func<Minion, bool>) (x => x.CardID != m.Id)) && this.FriendlyHand.Count<CardEntity>((Func<CardEntity, bool>) (x => x is MinionCardEntity minionCardEntity && minionCardEntity.Id == m.Id)) <= 1 ? (Action) null : (Action) (() =>
    {
      int num = this.DoubleIfGolden(2);
      foreach (Minion minion in this.FriendlySide)
        minion.IncreaseStats(num, num);
    });
  }
}
