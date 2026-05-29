// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.TimewarpedBassgill
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

public class TimewarpedBassgill(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_071";
  public const string Text = "<b>Deathrattle:</b> Summon the highest-Health minion from your hand and give it <b>Divine Shield</b> for this combat only.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon the 2 highest-Health minions from your hand and give them <b>Divine Shield</b> for this combat only.";

  public Action<Minion> GetDeathrattle() => TimewarpedBassgill.Deathrattle(this.golden);

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
          MinionCardEntity random = list.GroupBy<MinionCardEntity, int>((Func<MinionCardEntity, int>) (x => x.Data.baseHealth)).OrderBy<IGrouping<int, MinionCardEntity>, int>((Func<IGrouping<int, MinionCardEntity>, int>) (x => x.Key)).Last<IGrouping<int, MinionCardEntity>>().ToList<MinionCardEntity>().GetRandom<MinionCardEntity>();
          list.Remove(random);
          List<Minion> minionList = minion.TrySummonMinion((Summon) random.Data.Clone());
          if (minionList.Count > 0)
            random.CanSummon = false;
          foreach (Minion minion1 in minionList)
            minion1.div = 1;
        }
      }
    });
  }
}
