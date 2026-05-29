// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.TeronGorefiendHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class TeronGorefiendHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnStartOfCombat, IEntity, IOnAfterAttackStep
{
  private Minion? _clone;
  private Minion? _target;
  private int _targetDistanceDestroyedFromRight;

  public void OnCombatStartSetup()
  {
  }

  public bool IsActive() => this._clone != null;

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (!this.Data.IsActivated)
        return;
      this._target = this.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.game_id == this.Data.Data));
      if (this._target == null || this.FriendlyHeroPowers.Any<HeroPower>((Func<HeroPower, bool>) (x =>
      {
        if (x == this || !(x.CardID == this.CardID))
          return false;
        int? gameId3 = ((TeronGorefiendHeroPower) x)._target?.game_id;
        int gameId4 = this._target.game_id;
        return gameId3.GetValueOrDefault() == gameId4 & gameId3.HasValue;
      })))
        return;
      this._targetDistanceDestroyedFromRight = this.FriendlySide.Count<Minion>() - this._target.BoardPosition() - 1;
      using (this.Simulator.state.SummonScope.New("TeronGorefiend"))
      {
        this._clone = this._target.CloneAsShallowExactCopy();
        this.Simulator.Destroy(this._target, (Entity) this);
        this.Simulator.RegisterTrigger((Action) (() => this.TrySummonClone()), (IEntity) this);
      }
    });
  }

  private void TrySummonClone()
  {
    if (this._clone == null || this._target == null)
      return;
    int insertIndex = this.FriendlySide.Count<Minion>() - this._targetDistanceDestroyedFromRight;
    List<Minion> minionList = this.Simulator.TrySummonMinion((Summon) this._clone.CloneAsShallowExactCopy(), this.FriendlySide, insertIndex, (Entity) this);
    // ISSUE: explicit non-virtual call
    if ((minionList != null ? (__nonvirtual (minionList.Count) > 0 ? 1 : 0) : 0) == 0)
      return;
    this._clone = (Minion) null;
  }

  public void OnAfterAttackStep() => this.TrySummonClone();

  public void OnAfterStartOfCombat() => this.TrySummonClone();
}
