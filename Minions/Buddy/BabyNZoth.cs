// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.BabyNZoth
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

public class BabyNZoth(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_93_Buddy";
  public const string Text = "<b>Battlecry:</b> Make a friendly <b>Deathrattle</b> minion Golden.";
  public const string GoldenText = "<b>Battlecry:</b> Make all friendly <b>Deathrattle</b> minions Golden.";

  public Action OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.HasDeathrattle() && x.CanBeMadeGolden())).ToList<Minion>();
      if (this.golden)
      {
        foreach (Minion minion in list)
          minion.TryMakeGolden(true, (Entity) this);
      }
      else
      {
        Minion minion;
        if (!list.TryGetRandom<Minion>(out minion))
          return;
        minion.TryMakeGolden(true, (Entity) this);
      }
    });
  }
}
