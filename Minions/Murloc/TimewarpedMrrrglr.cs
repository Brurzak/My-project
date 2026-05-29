// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.TimewarpedMrrrglr
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Enchantments;
using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class TimewarpedMrrrglr(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_321";
  public const string Text = "<b>Start of Combat:</b> Give adjacent Murlocs the stats of all the minions in your hand.";
  public const string GoldenText = "<b>Start of Combat:</b> Give adjacent Murlocs the stats of all the minions in your hand twice.";

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
      List<Minion> list = new List<Minion>()
      {
        this.GetLeftNeighbor(),
        this.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null && x.IsAlive())).Cast<Minion>().ToList<Minion>();
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        foreach (Minion minion in list)
        {
          if (minion.IsMurloc())
            minion.IncreaseStats(attackBuff, healthBuff);
        }
      }
    });
  }
}
