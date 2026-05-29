// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.ExpertAviator
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

public class ExpertAviator(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_140";
  public const string Text = "<b>Rally:</b> Summon the highest- Attack minion from your hand for this combat only.";
  public const string GoldenText = "<b>Rally:</b> Summon the 2 highest-Attack minions from your hand for this combat only.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        List<IGrouping<int, MinionCardEntity>> list = this.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (x => x.CanSummon)).ToList<MinionCardEntity>().GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.attack())).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key)).ToList<IGrouping<int, MinionCardEntity>>();
        MinionCardEntity minionCardEntity;
        if (list.Any<IGrouping<int, MinionCardEntity>>() && list.Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity) && minionCardEntity != null && minion.TrySummonMinion((Summon) minionCardEntity.Data.Clone()).Count > 0)
          minionCardEntity.CanSummon = false;
      }
    });
  }
}
