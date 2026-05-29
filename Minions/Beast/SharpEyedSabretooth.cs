// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SharpEyedSabretooth
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SharpEyedSabretooth(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG33_846";
  public const string Text = "<b>Battlecry:</b> Give a friendly minion +{0} Attack. <i>(Improved by each friendly minion that died last combat!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give a friendly minion +{0} Attack. <i>(Improved by each friendly minion that died last combat!)</i>";

  private int FriendlyMinionsDeadLastCombatCounter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.FriendlyMinionsDeadLastCombatCounter : this.Simulator.state.Player.FriendlyMinionsDeadLastCombatCounter;
    }
  }

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
      int attackBuff = this.DoubleIfGolden(2 * (1 + this.FriendlyMinionsDeadLastCombatCounter));
      Minion minion;
      ref Minion local = ref minion;
      if (!list.TryGetRandom<Minion>(out local))
        return;
      minion.IncreaseStats(attackBuff, 0);
    });
  }
}
