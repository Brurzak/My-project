// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.CrochetedSungill
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class CrochetedSungill(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterAttackStep,
  IEntity
{
  public const string CardId = "BG32_MagicItem_960";
  private bool _summoned;

  public void OnAfterAttackStep()
  {
    if (this.FriendlySide.Count > 0 || this._summoned)
      return;
    MinionCardEntity[] array = this.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (m => m.CanSummon)).ToArray<MinionCardEntity>();
    MinionCardEntity minionCardEntity;
    if (!((IEnumerable<MinionCardEntity>) array).Any<MinionCardEntity>() || !((IEnumerable<MinionCardEntity>) array).GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.baseHealth)).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key)).Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity))
      return;
    this.Simulator.TrySummonMinion(Summon.FromMinion(minionCardEntity.Data.Clone()), this.FriendlySide, this.FriendlySide.Count, (Entity) this);
    this._summoned = true;
    minionCardEntity.CanSummon = false;
  }
}
