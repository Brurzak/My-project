// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.Dramaloc
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Enchantments;
using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class Dramaloc(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_143";
  public const string Text = "<b>Rally:</b> Give {0} friendly Murlocs the stats of the highest-Attack minion in your hand <i>(except Dramaloc)</i>.";
  public const string GoldenText = "<b>Rally:</b> Give {0} friendly Murlocs the stats of the highest-Attack minion in your hand, twice <i>(except Dramaloc)</i>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int attackBuff = 0;
      int healthBuff = 0;
      Enchantment enchantment = minion.Enchantments.FirstOrDefault<Enchantment>((Func<Enchantment, bool>) (e => e.AttachedTo == minion && e.CardID == "BG34_143e"));
      if (enchantment != null)
      {
        attackBuff = enchantment.ScriptDataNum1;
        healthBuff = enchantment.ScriptDataNum2;
      }
      else
      {
        List<MinionCardEntity> source = minion.FriendlyHandMinions(true);
        if (source.Any<MinionCardEntity>())
        {
          int highestAttack = source.Max<MinionCardEntity>((Func<MinionCardEntity, int>) (m => m.Data.baseAttack));
          MinionCardEntity minionCardEntity;
          if (source.Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (m => m.Data.baseAttack == highestAttack)).ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity))
          {
            attackBuff = minionCardEntity.Data.baseAttack;
            healthBuff = minionCardEntity.Data.baseHealth;
          }
        }
      }
      if (attackBuff <= 0 && healthBuff <= 0)
        return;
      int num = this.golden ? 2 : 1;
      for (int index1 = 0; index1 < num; ++index1)
      {
        List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsMurloc() && x.IsAlive() && x.CardID != "BG34_143")).ToList<Minion>();
        for (int index2 = 0; index2 < 2; ++index2)
        {
          Minion minion1;
          if (list.TryGetRandom<Minion>(out minion1))
          {
            minion1.IncreaseStats(attackBuff, healthBuff);
            list.Remove(minion1);
          }
        }
      }
    });
  }
}
