// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.TimewarpedMagnanimooseEnchantment
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Enchantments;

public class TimewarpedMagnanimooseEnchantment(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer) : Enchantment(cardId, simulator, controlledByPlayer), IDeathrattle, IEntity
{
  public const string CardId = "BACON_FAKE_Magnanimoose_Enchantment";

  public List<Minion> SummonedMinions { get; set; } = new List<Minion>();

  public Action<Minion>? GetDeathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      foreach (Minion summonedMinion in this.SummonedMinions)
      {
        Minion minion1 = summonedMinion.Clone(this.Simulator);
        minion.TrySummonMinion((Summon) minion1);
        this.AddCardToFriendlyHand((CardEntity) new MinionCardEntity(summonedMinion.Clone(this.Simulator), (Entity) this, this.Simulator));
      }
    });
  }

  public override Enchantment Clone(Simulator? simulator = null)
  {
    TimewarpedMagnanimooseEnchantment magnanimooseEnchantment = (TimewarpedMagnanimooseEnchantment) base.Clone(simulator);
    magnanimooseEnchantment.SummonedMinions = this.SummonedMinions.ToList<Minion>();
    return (Enchantment) magnanimooseEnchantment;
  }
}
