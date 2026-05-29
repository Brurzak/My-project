// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.TalentScout
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

public class TalentScout(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_HERO_105_Buddy";
  public const string Text = "<b>Battlecry:</b> Make a <b>Buddy</b> Golden.";
  public const string GoldenText = "<b>Battlecry:</b> Make 2 <b>Buddies</b> Golden.";

  public Action OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsBuddy && !x.golden)).Concat<Minion>(this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsBuddy && !x.golden))).ToList<Minion>();
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        Minion minion;
        if (list.TryGetRandom<Minion>(out minion))
        {
          minion.TryMakeGolden(true, (Entity) this);
          list.Remove(minion);
        }
      }
    });
  }
}
