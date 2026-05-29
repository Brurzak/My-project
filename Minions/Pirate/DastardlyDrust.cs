// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.DastardlyDrust
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class DastardlyDrust(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG32_234";
  public const string Text = "Whenever you get a Pirate, give your minions +{0}/+{1}. Give Golden ones +{2}/+{3} instead.";
  public const string GoldenText = "Whenever you get a Pirate, give your minions +{0}/+{1}. Give Golden ones +{2}/+{3} instead.";

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    if (!(card is MinionCardEntity minionCardEntity))
      return (Action) null;
    return !minionCardEntity.Data.IsPirate() ? (Action) null : (Action) (() =>
    {
      int num1 = this.DoubleIfGolden(2);
      int num2 = this.DoubleIfGolden(1);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion.IncreaseStats(minion.golden ? num1 * 2 : num1, minion.golden ? num2 * 2 : num2, (Entity) this);
    });
  }
}
