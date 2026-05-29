// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.FountainChiller
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

public class FountainChiller(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_145";
  public const string Text = "<b>Battlecry:</b> Give a friendly Murloc +{0}/+{1}. <i>(Improved by each different <b>Bonus Keyword</b> in your warband!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give a friendly Murloc +{0}/+{1}. <i>(Improved by each different <b>Bonus Keyword</b> in your warband!)</i>";

  public Action? OnBattlecry()
  {
    List<Minion> targets = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsMurloc() && x.IsAlive())).ToList<Minion>();
    if (targets.Count == 0)
      return (Action) null;
    int buffsCount = this.FriendlySide.Sum<Minion>((Func<Minion, int>) (x => !x.IsAlive() ? 0 : x.BonusKeywordCount()));
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(2) * (1 + buffsCount);
      int healthBuff = this.DoubleIfGolden(1) * (1 + buffsCount);
      Minion minion;
      if (!targets.TryGetRandom<Minion>(out minion))
        return;
      minion.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
