// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.GreenChromadrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class GreenChromadrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_636t";
  public const string Text = "<b>Battlecry:</b> Give your other Dragons +{0} Health.";
  public const string GoldenText = "<b>Battlecry:</b> Give your other Dragons +{0} Health.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsDragon() && x.IsAlive())).ToList<Minion>();
      int healthBuff = this.DoubleIfGolden(5);
      foreach (Minion minion in list)
        minion.IncreaseStats(0, healthBuff);
    });
  }
}
