// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.FreeFlyingFeathermane
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class FreeFlyingFeathermane(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  ISummonFromHand,
  IEntity,
  IOnFriendlyMinionDied,
  IOnAfterAttackStep
{
  public const string CardId = "BG27_014";
  public const string Text = "After a friendly Beast dies, summon this from your hand for this combat only.";
  public const string GoldenText = "After a friendly Beast dies, summon this from your hand for this combat only.";
  private int _summonIndex;

  public bool IsPendingSummon { get; set; }

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsBeast())
        return;
      this.IsPendingSummon = true;
      this._summonIndex = died.LastKnownPosition;
    });
  }

  public void OnAfterAttackStep()
  {
    if (!this.IsPendingSummon || !(this.FriendlyHand.FirstOrDefault<CardEntity>((Func<CardEntity, bool>) (x => x is MinionCardEntity minionCardEntity1 && minionCardEntity1.Data == this)) is MinionCardEntity minionCardEntity2) || !minionCardEntity2.CanSummon || this.Simulator.TrySummonMinion((Summon) this.Clone(), this.FriendlySide, this._summonIndex, (Entity) this).Count <= 0)
      return;
    minionCardEntity2.CanSummon = false;
  }
}
