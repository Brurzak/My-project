// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.ChoralMrrrglr
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Enchantments;
using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class ChoralMrrrglr(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG26_354";
  public const string Text = "<b>Start of Combat:</b> Gain the stats of all the minions in your hand.";
  public const string GoldenText = "<b>Start of Combat:</b> Gain the stats of all the minions in your hand twice.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int attackBuff = 0;
      int healthBuff = 0;
      Enchantment enchantment = this.Enchantments.FirstOrDefault<Enchantment>((Func<Enchantment, bool>) (e => e.AttachedTo == this && e.CardID == "BG26_354e"));
      if (enchantment != null)
      {
        attackBuff = enchantment.ScriptDataNum1;
        healthBuff = enchantment.ScriptDataNum2;
      }
      else
      {
        foreach (MinionCardEntity friendlyHandMinion in this.FriendlyHandMinions(true))
        {
          attackBuff += friendlyHandMinion.Data.baseAttack;
          healthBuff += friendlyHandMinion.Data.baseHealth;
        }
      }
      if (attackBuff <= 0 && healthBuff <= 0)
        return;
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        this.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
