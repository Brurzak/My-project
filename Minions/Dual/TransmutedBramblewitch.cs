// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.TransmutedBramblewitch
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class TransmutedBramblewitch(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG27_013";
  public const string Text = "<b>Rally:</b> Set the target's stats to 3/3. <i>(Once per combat.)</i>";
  public const string GoldenText = "<b>Rally:</b> Set the target's stats to 3/3. <i>(Twice per combat.)</i>";
  private int _attackEffectThisCombatCount;

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is TransmutedBramblewitch transmutedBramblewitch2) || transmutedBramblewitch2._attackEffectThisCombatCount >= this.DoubleIfGolden(1))
        return;
      target.SetStats(new int?(3), new int?(3));
      ++transmutedBramblewitch2._attackEffectThisCombatCount;
    });
  }
}
