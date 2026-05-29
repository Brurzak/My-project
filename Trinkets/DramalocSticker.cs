// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.DramalocSticker
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
namespace BobsBuddy.Trinkets;

public class DramalocSticker(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG35_MagicItem_754";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsMurloc())).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      int attackBuff = 0;
      Enchantment enchantment = this.Enchantments.FirstOrDefault<Enchantment>((Func<Enchantment, bool>) (e => e.AttachedTo == this && e.CardID == "BG35_MagicItem_754e"));
      if (enchantment != null)
      {
        attackBuff = enchantment.ScriptDataNum1;
      }
      else
      {
        List<MinionCardEntity> source = this.FriendlyHandMinions(true);
        if (source.Any<MinionCardEntity>())
        {
          int highestAttack = source.Max<MinionCardEntity>((Func<MinionCardEntity, int>) (m => m.Data.baseAttack));
          MinionCardEntity minionCardEntity;
          if (source.Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (m => m.Data.baseAttack == highestAttack)).ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity))
            attackBuff = minionCardEntity.Data.baseAttack;
        }
      }
      if (attackBuff <= 0)
        return;
      foreach (Minion minion in list)
        minion.IncreaseStats(attackBuff, 0);
    });
  }
}
