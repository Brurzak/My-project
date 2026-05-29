// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Duos.OrcEstraConductor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Duos;

public class OrcEstraConductor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGDUO_119";
  public const string Text = "<b>Battlecry:</b> Give a minion +{0}/+{1} <i>(Improved by each Orc-estra your team has played this game)</i>.";
  public const string GoldenText = "<b>Battlecry:</b> Give a minion +{0}/+{1} <i>(Improved by each Orc-estra your team has played this game)</i>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      int num = this.DoubleIfGolden(2);
      int by = Math.Max(this.ScriptDataNum1, 1) * num;
      Minion minion;
      ref Minion local = ref minion;
      if (!list.TryGetRandom<Minion>(out local))
        return;
      minion.IncreaseStats(by, (Entity) this);
    });
  }
}
