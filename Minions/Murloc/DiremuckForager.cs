// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.DiremuckForager
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class DiremuckForager(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity,
  IOnAfterAttackStep
{
  public const string CardId = "BG27_556";
  public const string Text = "<b>Start of Combat:</b> When you have space, summon the highest-Attack minion from your hand for this combat only.";
  public const string GoldenText = "<b>Start of Combat:</b> When you have space, summon the two highest-Attack minions from your hand for this combat only.";
  private int _tokensSummoned;

  public void OnCombatStartSetup()
  {
  }

  private int MaxSummons => !this.golden ? 1 : 2;

  public Action? OnStartOfCombat() => new Action(this.CheckForSummon);

  public void OnAfterAttackStep() => this.CheckForSummon();

  private void CheckForSummon()
  {
    if (this.FriendlySide.Count >= 7 || this._tokensSummoned >= this.MaxSummons)
      return;
    List<MinionCardEntity> list = this.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (x => x.CanSummon)).ToList<MinionCardEntity>();
    if (this.FriendlySide.Count == 6)
    {
      IOrderedEnumerable<IGrouping<int, MinionCardEntity>> source = list.GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.attack())).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key));
      MinionCardEntity minionCardEntity;
      if (!source.Any<IGrouping<int, MinionCardEntity>>() || !source.Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity) || minionCardEntity == null)
        return;
      if (this.Simulator.TrySummonMinion(new Summon(minionCardEntity.Data.Clone()), this.FriendlySide, this.BoardPosition() + 1, (Entity) this).Count > 0)
        minionCardEntity.CanSummon = false;
      ++this._tokensSummoned;
    }
    else
    {
      if (this.FriendlySide.Count > 5)
        return;
      if (this._tokensSummoned == 0 && this.golden)
      {
        IOrderedEnumerable<IGrouping<int, MinionCardEntity>> source1 = list.GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.attack())).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key));
        MinionCardEntity minionCardEntity1;
        if (!source1.Any<IGrouping<int, MinionCardEntity>>() || !source1.Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity1) || minionCardEntity1 == null)
          return;
        if (this.Simulator.TrySummonMinion(new Summon(minionCardEntity1.Data.Clone()), this.FriendlySide, this.BoardPosition() + 1, (Entity) this).Count > 0)
          minionCardEntity1.CanSummon = false;
        ++this._tokensSummoned;
        IOrderedEnumerable<IGrouping<int, MinionCardEntity>> source2 = this.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (x => x.CanSummon)).ToList<MinionCardEntity>().GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.attack())).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key));
        MinionCardEntity minionCardEntity2;
        if (!source2.Any<IGrouping<int, MinionCardEntity>>() || !source2.Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity2) || minionCardEntity2 == null)
          return;
        if (this.Simulator.TrySummonMinion(new Summon(minionCardEntity2.Data.Clone()), this.FriendlySide, this.BoardPosition() + 2, (Entity) this).Count > 0)
          minionCardEntity1.CanSummon = false;
        ++this._tokensSummoned;
      }
      else
      {
        IOrderedEnumerable<IGrouping<int, MinionCardEntity>> source = list.GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.attack())).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key));
        MinionCardEntity minionCardEntity;
        if (!source.Any<IGrouping<int, MinionCardEntity>>() || !source.Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity) || minionCardEntity == null)
          return;
        if (this.Simulator.TrySummonMinion(new Summon(minionCardEntity.Data.Clone()), this.FriendlySide, this.BoardPosition() + 1, (Entity) this).Count > 0)
          minionCardEntity.CanSummon = false;
        ++this._tokensSummoned;
      }
    }
  }
}
