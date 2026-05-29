// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.OzumatHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class OzumatHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnStartOfCombat, IEntity, IOnAfterAttackStep
{
  private bool _tokenSummoned;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat() => new Action(this.CheckForTentacular);

  public void OnAfterAttackStep() => this.CheckForTentacular();

  private void CheckForTentacular()
  {
    if (this.FriendlySide.Count >= 7 || this._tokenSummoned)
      return;
    Simulator simulator = this.Simulator;
    Summon summon = new Summon("BG23_HERO_201pt");
    summon.SetStats = new (int, int)?((this.Data.Data, this.Data.Data));
    List<Minion> friendlySide = this.FriendlySide;
    int count = this.FriendlySide.Count;
    simulator.TrySummonMinion(summon, friendlySide, count, (Entity) this);
    this._tokenSummoned = true;
  }
}
