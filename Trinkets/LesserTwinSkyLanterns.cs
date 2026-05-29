// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserTwinSkyLanterns
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserTwinSkyLanterns(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionSummoned,
  IEntity,
  IOnStartOfCombat,
  IOnAfterAttackStep
{
  public const string CardId = "BG30_MagicItem_822";
  private Summon? _summon;
  private bool _summoned;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat() => new Action(this.CheckForSummon);

  public void OnAfterAttackStep() => this.CheckForSummon();

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (this._summon != null)
        return;
      this._summon = Summon.FromMinion(summoned.Clone());
    });
  }

  private void CheckForSummon()
  {
    if (this.FriendlySide.Count >= 7 || this._summon == null || this._summoned)
      return;
    this.Simulator.TrySummonMinion(this._summon, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
    this._summoned = true;
  }
}
