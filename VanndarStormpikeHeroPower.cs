// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.VanndarStormpikeHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class VanndarStormpikeHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnStartOfCombat, IEntity, IOnAfterAttackStep
{
  private bool _tokenSummoned;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat() => new Action(this.CheckForSummon);

  public void OnAfterAttackStep() => this.CheckForSummon();

  private void CheckForSummon()
  {
    if (this.FriendlySide.Count >= 7 || this._tokenSummoned || this.Simulator.Turn < 7)
      return;
    Minion highestHealthMinion = this.Simulator.GetHighestHealthMinion(this.FriendlySide);
    if (highestHealthMinion == null)
      return;
    this.Simulator.TrySummonMinion(new Summon(highestHealthMinion.Clone()), this.FriendlySide, highestHealthMinion.BoardPosition() + 1, (Entity) this);
    this._tokenSummoned = true;
  }
}
