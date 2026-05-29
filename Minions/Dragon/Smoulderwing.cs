// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Smoulderwing
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class Smoulderwing(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_309t";
  public const string Text = "<b>Battlecry:</b> Give a Dragon +5 Attack.";
  public const string GoldenText = "<b>Battlecry:</b> Give a Dragon +10 Attack.";

  public Action? OnBattlecry()
  {
    List<Minion> targets = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsDragon() && x.IsAlive())).ToList<Minion>();
    return targets.Count == 0 ? (Action) null : (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(5);
      targets.GetRandom<Minion>().IncreaseStats(attackBuff, 0);
    });
  }
}
