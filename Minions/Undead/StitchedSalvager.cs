// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.StitchedSalvager
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class StitchedSalvager(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IOnStartOfCombat
{
  public const string CardId = "BG31_999";
  public const string Text = "<b>Start of Combat:</b> Destroy the minion to the left. <b>Deathrattle:</b> Summon an exact copy of it. <i>(Except Stitched Salvager.)</i>";
  public const string GoldenText = "<b>Start of Combat:</b> Destroy adjacent minions. <b>Deathrattle:</b> Summon exact copies of them. <i>(Except Stitched Salvager.)</i>";
  private List<Minion>? _clones;

  public void OnCombatStartSetup() => this._clones = new List<Minion>();

  public bool DeathrattleInitialized()
  {
    List<Minion> clones = this._clones;
    // ISSUE: explicit non-virtual call
    return clones != null && __nonvirtual (clones.Count) > 0;
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> source = new List<Minion>();
      source.Add(this.GetLeftNeighbor());
      if (this.golden)
        source.Add(this.GetRightNeighbor());
      if (!source.Any<Minion>())
        return;
      foreach (Minion target in source)
      {
        if (target != null && target.CardID != "BG31_999")
        {
          this._clones?.Add(target.CloneAsShallowExactCopy());
          this.Simulator.Destroy(target, (Entity) this);
        }
      }
    });
  }

  public Action<Minion> GetDeathrattle() => this.Deathrattle();

  public Action<Minion> Deathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      int lastKnownPosition = minion.LastKnownPosition;
      foreach (Minion minion1 in this._clones ?? new List<Minion>())
        minion.Simulator.TrySummonMinion((Summon) minion1.CloneAsShallowExactCopy(), minion.FriendlySide, lastKnownPosition, (Entity) minion);
    });
  }
}
