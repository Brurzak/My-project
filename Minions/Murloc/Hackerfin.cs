// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.Hackerfin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class Hackerfin(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_148";
  public const string Text = "<b>Battlecry:</b> Give your other minions +{0}/+{1}. <i>(Improved by each different <b>Bonus Keyword</b> in your warband!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give your other minions +{0}/+{1}. <i>(Improved by each different <b>Bonus Keyword</b> in your warband!)</i>";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      HashSet<string> stringSet = new HashSet<string>();
      foreach (Minion minion in list)
      {
        if (minion.taunt)
          stringSet.Add("Taunt");
        if (minion.hasDiv)
          stringSet.Add("DivineShield");
        if (minion.poisonous)
          stringSet.Add("Poisonous");
        if (minion.venomous)
          stringSet.Add("Venomous");
        if (minion.windfury)
          stringSet.Add("Windfury");
        if (minion.megaWindfury)
          stringSet.Add("MegaWindfury");
        if (minion.stealth)
          stringSet.Add("Stealth");
        if (minion.reborn)
          stringSet.Add("Reborn");
        if (minion.cleave)
          stringSet.Add("Cleave");
      }
      int attackBuff = this.DoubleIfGolden(1) * (1 + stringSet.Count);
      int healthBuff = this.DoubleIfGolden(2) * (1 + stringSet.Count);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(attackBuff, healthBuff, (Entity) this);
    });
  }
}
