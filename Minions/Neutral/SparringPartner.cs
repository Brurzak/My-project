// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.SparringPartner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class SparringPartner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG_AT_069";
  public const string Text = "<b>Taunt</b> <b>Battlecry:</b> Give a minion <b>Taunt</b>.";
  public const string GoldenText = "<b>Taunt</b> <b>Battlecry:</b> Give a minion <b>Taunt</b>.";

  public Action? OnBattlecry()
  {
    List<Minion> targets = this.FriendlySide.Concat<Minion>((IEnumerable<Minion>) this.OpposingSide).Where<Minion>((Func<Minion, bool>) (x => !x.taunt && x.IsAlive())).ToList<Minion>();
    return targets.Count == 0 ? (Action) null : (Action) (() => targets.GetRandom<Minion>().taunt = true);
  }
}
