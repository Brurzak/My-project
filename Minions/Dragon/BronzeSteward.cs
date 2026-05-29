// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.BronzeSteward
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class BronzeSteward(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG32_824";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Give your other Dragons +10 Attack.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Give your other Dragons +20 Attack.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive() && x.IsDragon())).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      foreach (Minion minion in list)
        minion.IncreaseStats(this.DoubleIfGolden(10), 0);
    });
  }

  public Action<Minion> GetDeathrattle() => BronzeSteward.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsDragon())).ToList<Minion>().ForEach((Action<Minion>) (target => target.IncreaseStats(golden ? 20 : 10, 0))));
  }
}
