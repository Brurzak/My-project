// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BoomController
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BoomController(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionDied,
  IEntity,
  IOnStartOfCombat,
  IOnAfterAttackStep,
  IOnFriendlyMinionSummoned
{
  public const string CardId = "BG30_MagicItem_440";
  private Summon? _summon;
  private bool _summoned;

  public void OnCombatStartSetup()
  {
    this.FriendlyMechs = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsMech())).Select<Minion, Minion>((Func<Minion, Minion>) (mech => mech.CloneAsShallowExactCopy())).ToList<Minion>();
  }

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsMech())
        return;
      this.FriendlyMechs?.Add(summoned.CloneAsShallowExactCopy());
    });
  }

  private List<Minion>? FriendlyMechs { get; set; }

  public Action? OnStartOfCombat() => new Action(this.CheckForSummon);

  public void OnAfterAttackStep() => this.CheckForSummon();

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsMech() || this._summon != null)
        return;
      List<Minion> friendlyMechs = this.FriendlyMechs;
      Minion minion = friendlyMechs != null ? friendlyMechs.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.OriginalMinion == died.OriginalMinion)) : (Minion) null;
      if (minion == null)
        return;
      this._summon = Summon.FromMinion(minion.CloneAsShallowExactCopy());
    });
  }

  private void CheckForSummon()
  {
    if (this.FriendlySide.Count == 7 || this._summon == null || this._summoned)
      return;
    this.Simulator.TrySummonMinion(this._summon, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
    this._summoned = true;
  }
}
