// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.WingedChimera
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class WingedChimera(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG29_844";
  public const string Text = "Whenever this takes damage, give a friendly minion of each type +1/+1 permanently. <i>(Twice per combat.)</i>";
  public const string GoldenText = "Whenever this takes damage, give a friendly minion of each type +2/+2 permanently. <i>(Twice per combat.)</i>";
  private int _increasedStatsThisCombatCount;

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() =>
    {
      if (this._increasedStatsThisCombatCount >= 2)
        return;
      ++this._increasedStatsThisCombatCount;
      foreach (Minion minion in this.FriendlySide.GetRandomPerRace())
        minion.IncreaseStats(this.DoubleIfGolden(1), (Entity) this);
    });
  }
}
