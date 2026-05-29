// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.FishOfNzoth_Skin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class FishOfNzoth_Skin(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "TB_BaconShop_HP_105t_SKIN_A";

  public Action OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (this.IsDead())
        return;
      string cardId = died.CardID;
      if (cardId == "TB_BaconShop_HP_105t_SKIN_A" || cardId == "TB_BaconShop_HP_105t")
        return;
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        foreach (IDeathrattle trigger in died.GetTriggers<IDeathrattle>())
        {
          Action<Minion> deathrattle = trigger.GetDeathrattle();
          if (deathrattle != null)
            this.AdditionalDeathrattles.Add(deathrattle);
        }
        if (died.AdditionalDeathrattles.Count > 0)
          this.AdditionalDeathrattles.AddRange((IEnumerable<Action<Minion>>) died.AdditionalDeathrattles);
      }
    });
  }
}
