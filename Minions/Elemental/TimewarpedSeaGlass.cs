// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.TimewarpedSeaGlass
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class TimewarpedSeaGlass(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMinionMadeGolden,
  IEntity,
  IOnRally
{
  public const string CardId = "BG34_Giant_110";
  public const string Text = "<b>Divine Shield</b> <b>Rally:</b> Double this minion's stats. <i>({1} times per combat.)</i>2[x]<b>Divine Shield</b> <b>Rally:</b> Double this minion's stats. <i>({0} left!)</i>";
  public const string GoldenText = "<b>Divine Shield</b> <b>Rally:</b> Triple this minion's stats. <i>({1} times per combat.)</i>2[x]<b>Divine Shield</b> <b>Rally:</b> Triple this minion's stats. <i>({0} left!)</i>";
  private int _attackEffectThisCombatCount;

  public Action? OnMinionMadeGolden() => (Action) (() => this._attackEffectThisCombatCount = 0);

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is TimewarpedSeaGlass timewarpedSeaGlass2) || timewarpedSeaGlass2._attackEffectThisCombatCount >= 2)
        return;
      int num = this.golden ? 2 : 1;
      this.IncreaseStats(this.attack() * num, this.health() * num, (Entity) this);
      ++timewarpedSeaGlass2._attackEffectThisCombatCount;
    });
  }
}
