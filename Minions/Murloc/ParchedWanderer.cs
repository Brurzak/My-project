// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.ParchedWanderer
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

public class ParchedWanderer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG30_756";
  public const string Text = "<b>Battlecry:</b> Give a Murloc +2/+3 and <b>Taunt</b>.";
  public const string GoldenText = "<b>Battlecry:</b> Give a Murloc +4/+6 and <b>Taunt</b>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsMurloc() && x.IsAlive())).Concat<Minion>(this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsMurloc() && x.IsAlive()))).ToList<Minion>();
      int attackBuff = this.DoubleIfGolden(2);
      int healthBuff = this.DoubleIfGolden(3);
      Minion minion;
      ref Minion local = ref minion;
      if (!list.TryGetRandom<Minion>(out local))
        return;
      minion.IncreaseStats(attackBuff, healthBuff);
      minion.taunt = true;
    });
  }
}
