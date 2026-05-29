// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.FlobbidinousFloopHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class FlobbidinousFloopHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnStartOfCombat, IEntity
{
  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (!this.Data.IsActivated)
        return;
      Minion minion1 = this.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.game_id == this.Data.Data));
      if (minion1 == null)
        return;
      if (this.TeammateSide == null)
        throw new UnsupportedInteractionException("Teammate's board is unknown", (Entity) this);
      if (this.TeammateSide.Count <= 0)
        return;
      IGrouping<int, \u003C\u003Ef__AnonymousType0<int, Minion>> source = this.TeammateSide.Select(x => new
      {
        Tier = x.tier,
        Minion = x
      }).GroupBy(x => x.Tier).OrderByDescending<IGrouping<int, \u003C\u003Ef__AnonymousType0<int, Minion>>, int>(x => x.Key).FirstOrDefault<IGrouping<int, \u003C\u003Ef__AnonymousType0<int, Minion>>>();
      var data;
      if (source == null || !source.ToList().TryGetRandom(out data))
        return;
      Minion minion2 = data.Minion.Clone(this.Simulator);
      int insertIndex = minion1.BoardPosition();
      this.FriendlySide.Remove(minion1);
      this.Simulator.TrySummonMinion((Summon) minion2, this.FriendlySide, insertIndex, (Entity) this);
    });
  }
}
