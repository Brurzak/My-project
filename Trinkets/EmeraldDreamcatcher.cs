// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.EmeraldDreamcatcher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class EmeraldDreamcatcher(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_542";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      int num = list.Select<Minion, int>((Func<Minion, int>) (m => m.attack())).Max();
      foreach (Minion minion in list.Where<Minion>((Func<Minion, bool>) (x => x.IsDragon())))
        minion.SetStats(new int?(num), new int?(minion.health()));
    });
  }
}
