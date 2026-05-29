// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.TideOracleMorgl
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class TideOracleMorgl(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterAttack,
  IEntity
{
  public const string CardId = "BG27_513";
  public const string Text = "<b><b>Poisonous</b>.</b> When this attacks and kills a minion, give its maximum stats to a minion in your hand.";
  public const string GoldenText = "<b><b>Poisonous</b>.</b> When this attacks and kills a minion, give double its maximum stats to a minion in your hand.";

  public Action? OnAfterAttack(Minion target)
  {
    return (Action) (() =>
    {
      if (!target.IsDead())
        return;
      int attackBuff = this.DoubleIfGolden(target.maxAttack + target.attackBonus());
      int healthBuff = this.DoubleIfGolden(target.maxHealth + target.healthBonus());
      MinionCardEntity minionCardEntity;
      if (!this.FriendlyHandMinions(false).TryGetRandom<MinionCardEntity>(out minionCardEntity))
        return;
      minionCardEntity.Data.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
