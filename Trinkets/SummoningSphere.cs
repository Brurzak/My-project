// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.SummoningSphere
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class SummoningSphere(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BGDUO_MagicItem_003";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.TeammateSide == null)
        throw new UnsupportedInteractionException("Teammate's board is unknown", (Entity) this);
      if (this.TeammateSide.Count == 0)
        return;
      IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>> source = this.TeammateSide.Select(x => new
      {
        Health = x.health(),
        Minion = x
      }).GroupBy(x => x.Health).OrderByDescending<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>, int>(x => x.Key).FirstOrDefault<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>>();
      var data;
      if (source == null || !source.ToList().TryGetRandom(out data))
        return;
      this.Simulator.TrySummonMinion((Summon) data.Minion.Clone(this.Simulator), this.FriendlySide, this.FriendlySide.Count, (Entity) this);
    });
  }
}
