// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.StharaSticker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class StharaSticker(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionDied,
  IEntity,
  IOnStartOfCombat,
  IOnAfterAttackStep,
  IOnFriendlyMinionSummoned
{
  public const string CardId = "BG32_MagicItem_907";
  private Summon? _summon;
  private bool _summoned;

  public void OnCombatStartSetup()
  {
    this.FriendlyDemons = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsDemon())).Select<Minion, Minion>((Func<Minion, Minion>) (demon => demon.Clone())).ToList<Minion>();
  }

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsDemon())
        return;
      this.FriendlyDemons?.Add(summoned.Clone());
    });
  }

  private List<Minion>? FriendlyDemons { get; set; }

  public Action? OnStartOfCombat() => new Action(this.CheckForSummon);

  public void OnAfterAttackStep() => this.CheckForSummon();

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsDemon() || this._summon != null)
        return;
      List<Minion> friendlyDemons = this.FriendlyDemons;
      Minion minion = friendlyDemons != null ? friendlyDemons.FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.OriginalMinion == died.OriginalMinion)) : (Minion) null;
      if (minion == null)
        return;
      this._summon = new Summon(minion.CardID, minion.golden)
      {
        SetStats = new (int, int)?((minion.maxAttack, minion.maxHealth))
      };
    });
  }

  private void CheckForSummon()
  {
    if (this.FriendlySide.Count > 0 || this._summon == null || this._summoned)
      return;
    this.Simulator.TrySummonMinion(this._summon, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
    this._summoned = true;
  }
}
