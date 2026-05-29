// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.ColdlightSeer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class ColdlightSeer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG_EX1_103";
  public const string Text = "<b>Battlecry:</b> Give your other Murlocs +2 Health.";
  public const string GoldenText = "<b>Battlecry:</b> Give your other Murlocs +4 Health.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsMurloc() && x.IsAlive())).ToList<Minion>();
      int healthBuff = this.DoubleIfGolden(2);
      foreach (Minion minion in list)
        minion.IncreaseStats(0, healthBuff);
    });
  }
}
