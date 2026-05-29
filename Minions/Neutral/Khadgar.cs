// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Khadgar
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class Khadgar(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG_DAL_575";
  public const string Text = "Your cards that summon minions summon twice as many.";
  public const string GoldenText = "Your cards that summon minions summon three times as many.";
  private readonly List<Minion> _summonedMinions = new List<Minion>();

  public override Action? OnMinionSummonedByFriendly(
    Minion summoned,
    Entity? source,
    bool wasReborn)
  {
    if (source == this)
      return (Action) null;
    if (!(source is Minion))
      return (Action) null;
    return this._summonedMinions.Contains(summoned.OriginalMinion) ? (Action) null : (Action) (() =>
    {
      this._summonedMinions.Add(summoned.OriginalMinion);
      this.Simulator.TrySummonMinion((Summon) summoned.OriginalMinion.Clone(), summoned.FriendlySide, summoned.BoardPosition() + 1, (Entity) this);
      if (!this.golden)
        return;
      this.Simulator.TrySummonMinion((Summon) summoned.OriginalMinion.Clone(), summoned.FriendlySide, summoned.BoardPosition() + 1, (Entity) this);
    });
  }
}
