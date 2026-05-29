// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.TimewarpedNelliesShipEnchantment
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using System;

#nullable enable
namespace BobsBuddy.Enchantments;

public class TimewarpedNelliesShipEnchantment(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer) : Enchantment(cardId, simulator, controlledByPlayer), IDeathrattle, IEntity
{
  public const string CardId = "BACON_FAKE_NelliesShip_Enchantment";

  public Action<Minion>? GetDeathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      string cardId1;
      if (this.ScriptDataNum1 != 0 && Cards.DbfIdToCardId.TryGetValue(this.ScriptDataNum1, out cardId1))
      {
        minion.TrySummonMinion((Summon) cardId1);
        this.AddMinionToFriendlyHand(cardId1);
      }
      string cardId2;
      if (this.ScriptDataNum2 == 0 || !Cards.DbfIdToCardId.TryGetValue(this.ScriptDataNum2, out cardId2))
        return;
      minion.TrySummonMinion((Summon) cardId2);
      this.AddMinionToFriendlyHand(cardId2);
    });
  }
}
