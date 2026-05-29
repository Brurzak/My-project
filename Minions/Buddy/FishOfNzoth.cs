// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.FishOfNzoth
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class FishOfNzoth(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "TB_BaconShop_HP_105t";
  public const string Text = "After a different friendly <b>Deathrattle</b> minion dies in combat, gain its <b>Deathrattle</b>.";
  public const string GoldenText = "After a different friendly <b>Deathrattle</b> minion dies in combat, gain its <b>Deathrattle</b> twice.";

  public Action OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (this.IsDead())
        return;
      string cardId = died.CardID;
      if (cardId == "TB_BaconShop_HP_105t" || cardId == "TB_BaconShop_HP_105t_SKIN_A")
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
