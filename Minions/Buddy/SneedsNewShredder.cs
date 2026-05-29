// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.SneedsNewShredder
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class SneedsNewShredder(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG21_HERO_030t";

  public Action<Minion> GetDeathrattle() => SneedsNewShredder.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<MinionCardEntity> list = minion.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (m => m.CanSummon)).ToList<MinionCardEntity>();
      int num = minion.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        if (list.Count > 0)
        {
          MinionCardEntity minionCardEntity;
          if (!list.GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.baseHealth)).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key)).Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity))
            break;
          list.Remove(minionCardEntity);
          List<Minion> minionList = minion.TrySummonMinion((Summon) minionCardEntity.Data.Clone());
          if (minionList.Count > 0)
          {
            minionList[0].div = 1;
            minionCardEntity.CanSummon = false;
          }
        }
      }
    });
  }
}
