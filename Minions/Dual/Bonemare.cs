// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.Bonemare
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class Bonemare(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG26_ICC_705";
  public const string Text = "<b>Battlecry:</b> Give a friendly minion +4/+4 and <b>Taunt</b>.";

  public Action? OnBattlecry()
  {
    List<Minion> targets = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => !x.taunt && x.IsAlive())).ToList<Minion>();
    if (targets.Count == 0)
      targets = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
    if (targets.Count == 0)
      return (Action) null;
    int stats = this.DoubleIfGolden(4);
    return (Action) (() =>
    {
      Minion random = targets.GetRandom<Minion>();
      random.taunt = true;
      random.IncreaseStats(stats, stats, (Entity) this);
    });
  }
}
