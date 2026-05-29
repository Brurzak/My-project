// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.TrustyCrowbar
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class TrustyCrowbar(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnCardAddedToFriendlyHand,
  IEntity
{
  public const string CardId = "BG35_MagicItem_713";

  public Action? OnCardAddedToFriendlyHand(CardEntity card, Entity? source)
  {
    return (Action) (() =>
    {
      if (!(card is MinionCardEntity minionCardEntity2) || !minionCardEntity2.Data.IsPirate())
        return;
      this.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.IsAlive()))?.IncreaseStats(this.ScriptDataNum1, this.ScriptDataNum2);
    });
  }
}
