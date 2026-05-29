// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.TinyfinOnesie
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

public class TinyfinOnesie(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_441";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count == 0)
        return;
      List<MinionCardEntity> list = this.FriendlyHandMinions(true).ToList<MinionCardEntity>();
      MinionCardEntity minionCardEntity;
      if (list.Count == 0 || !list.GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (m => m.Data.health())).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (m => m.Key)).Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().TryGetRandom<MinionCardEntity>(out minionCardEntity) || minionCardEntity.Data.health() <= 0)
        return;
      this.FriendlySide[0].IncreaseStats(minionCardEntity.Data.attack(), minionCardEntity.Data.health());
    });
  }
}
