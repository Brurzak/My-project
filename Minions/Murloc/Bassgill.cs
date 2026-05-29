// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.Bassgill
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

public class Bassgill(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_350";
  public const string Text = "<b>Deathrattle:</b> Summon the highest-Health Murloc from your hand for this combat only.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon the 2 highest-Health Murlocs from your hand for this combat only.";

  public Action<Minion> GetDeathrattle() => Bassgill.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<MinionCardEntity> list = minion.FriendlyHandMinions(false).Where<MinionCardEntity>((Func<MinionCardEntity, bool>) (m => m.CanSummon && m.Data.IsMurloc())).ToList<MinionCardEntity>();
      int num = minion.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        if (list.Count > 0)
        {
          MinionCardEntity random = list.GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.baseHealth)).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key)).Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().GetRandom<MinionCardEntity>();
          list.Remove(random);
          if (minion.TrySummonMinion((Summon) random.Data.Clone()).Count > 0)
            random.CanSummon = false;
        }
      }
    });
  }
}
