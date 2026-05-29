// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.SoulJuggler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class SoulJuggler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BGS_002";
  public const string Text = "After a friendly Demon dies, deal {0} damage to the highest-Health enemy minion.";
  public const string GoldenText = "After a friendly Demon dies, deal {0} damage to the highest-Health enemy minion, twice.";

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return !died.IsDemon() ? (Action) null : (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        List<Minion> list = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
        if (list.Count == 0)
          break;
        int highestHealth = list.Max<Minion>((Func<Minion, int>) (x => x.health()));
        Minion target;
        if (list.Where<Minion>((Func<Minion, bool>) (x => x.health() == highestHealth)).ToList<Minion>().TryGetRandom<Minion>(out target))
          this.Simulator.ProcessDamage(4, target, (Entity) this);
      }
    });
  }
}
