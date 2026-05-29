// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.GhoulAcabra
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class GhoulAcabra(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG29_863";
  public const string Text = "After a friendly <b>Deathrattle</b> minion dies, give your minions +2/+2 permanently.";
  public const string GoldenText = "After a friendly <b>Deathrattle</b> minion dies, give your minions +4/+4 permanently.";

  public Action? OnAfterFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.HasDeathrattle())
        return;
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      int attackBuff = this.DoubleIfGolden(2);
      int healthBuff = this.DoubleIfGolden(2);
      Action<Minion> action = (Action<Minion>) (minion => minion.IncreaseStats(attackBuff, healthBuff, (Entity) this));
      list.ForEach(action);
    });
  }
}
