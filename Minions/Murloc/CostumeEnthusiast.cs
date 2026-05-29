// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.CostumeEnthusiast
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

public class CostumeEnthusiast(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_142";
  public const string Text = "<b>Divine Shield</b> <b>Start of Combat:</b> Gain the Attack of the highest-Attack minion in your hand.";
  public const string GoldenText = "<b>Divine Shield</b>. <b>Start of Combat:</b> Gain double the Attack of the highest-Attack minion in your hand.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int attackBuff = 0;
      int num = 0;
      Enchantment enchantment = this.Enchantments.FirstOrDefault<Enchantment>((Func<Enchantment, bool>) (e => e.AttachedTo == this && e.CardID == "BG34_142e"));
      if (enchantment != null)
      {
        attackBuff = enchantment.ScriptDataNum1;
        num = enchantment.ScriptDataNum2;
      }
      else
      {
        List<MinionCardEntity> source = this.FriendlyHandMinions(true);
        if (source.Any<MinionCardEntity>())
        {
          int highestAttack = source.Max<MinionCardEntity>((Func<MinionCardEntity, int>) (m => m.Data.baseAttack));
          MinionCardEntity minionCardEntity;
          if (source.Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (m => m.Data.baseAttack == highestAttack)).ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity))
          {
            attackBuff = minionCardEntity.Data.baseAttack;
            num = minionCardEntity.Data.baseHealth;
          }
        }
      }
      if (attackBuff <= 0 && num <= 0)
        return;
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        this.IncreaseStats(attackBuff, 0);
    });
  }
}
