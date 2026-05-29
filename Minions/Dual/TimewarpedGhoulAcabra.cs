// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.TimewarpedGhoulAcabra
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class TimewarpedGhoulAcabra(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_609";
  public const string Text = "After you trigger a <b>Deathrattle</b>, give your minions +{0}/+{1} permanently.";
  public const string GoldenText = "After you trigger a <b>Deathrattle</b>, give your minions +{0}/+{1} permanently.";

  public Action? OnFriendlyDeathrattle()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      int attackBuff = this.DoubleIfGolden(3);
      int healthBuff = this.DoubleIfGolden(2);
      Action<Minion> action = (Action<Minion>) (minion => minion.IncreaseStats(attackBuff, healthBuff, (Entity) this));
      list.ForEach(action);
    });
  }
}
