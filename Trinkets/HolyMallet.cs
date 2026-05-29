// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.HolyMallet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class HolyMallet(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_902";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count == 0)
        return;
      Minion minion1 = this.FriendlySide[0];
      Minion minion2 = this.FriendlySide.Count > 1 ? this.FriendlySide.Last<Minion>() : (Minion) null;
      if (minion1.IsAlive())
        minion1.div = 1;
      if (minion2 == null || !minion2.IsAlive())
        return;
      minion2.div = 1;
    });
  }
}
