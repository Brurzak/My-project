// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.AssistantGuard
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

public class AssistantGuard(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG29_845";
  public const string Text = "<b>Battlecry:</b> Give a minion <b>Taunt</b>. Then give your <b>Taunt</b> minions +2/+3.";
  public const string GoldenText = "<b>Battlecry:</b> Give a minion <b>Taunt</b>. Then give your <b>Taunt</b> minions +4/+6.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      Minion minion1;
      if (this.FriendlySide.Concat<Minion>((IEnumerable<Minion>) this.OpposingSide).Where<Minion>((Func<Minion, bool>) (x => !x.taunt && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
        minion1.taunt = true;
      int attackBuff = this.DoubleIfGolden(2);
      int healthBuff = this.DoubleIfGolden(3);
      foreach (Minion minion2 in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.taunt && x.IsAlive())).ToList<Minion>())
        minion2.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
