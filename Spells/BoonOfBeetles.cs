// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.BoonOfBeetles
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Spells;

public class BoonOfBeetles(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity,
  IOnAfterAttackStep
{
  public const string CardId = "BG28_603";
  private readonly Summon _summon = new Summon("BG28_603t", giveTaunt: true);
  private int _tokensSummoned;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat() => new Action(this.CheckForBeetles);

  public void OnAfterAttackStep() => this.CheckForBeetles();

  private void CheckForBeetles()
  {
    if (this.FriendlySide.Count >= 7 || this._tokensSummoned >= 2)
      return;
    if (this.FriendlySide.Count == 6)
    {
      this.Simulator.TrySummonMinions((IEnumerable<Summon>) new Summon[1]
      {
        this._summon
      }, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
      ++this._tokensSummoned;
    }
    else
    {
      if (this.FriendlySide.Count > 5)
        return;
      if (this._tokensSummoned == 0)
      {
        this.Simulator.TrySummonMinions((IEnumerable<Summon>) new Summon[1]
        {
          this._summon
        }, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
        ++this._tokensSummoned;
      }
      if (this._tokensSummoned != 1)
        return;
      this.Simulator.TrySummonMinions((IEnumerable<Summon>) new Summon[1]
      {
        this._summon
      }, this.FriendlySide, this.FriendlySide.Count, (Entity) this);
      ++this._tokensSummoned;
    }
  }
}
