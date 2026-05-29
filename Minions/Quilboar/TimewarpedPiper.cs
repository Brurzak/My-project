// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedPiper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedPiper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMinionMadeGolden,
  IEntity,
  IOnTakeDamage
{
  public const string CardId = "BG34_Giant_069";
  public const string Text = "Whenever this takes damage, your <b>Blood Gems</b> give an extra +{1} Attack this game. <i>({2} times per combat.)</i>3[x]Whenever this takes damage, your <b>Blood Gems</b> give an extra +{1} Attack this game. <i>({0} left!)</i>";
  public const string GoldenText = "Whenever this takes damage, your <b>Blood Gems</b> give an extra +{1} Attack this game. <i>({2} times per combat.)</i>3[x]Whenever this takes damage, your <b>Blood Gems</b> give an extra +{1} Attack this game. <i>({0} left!)</i>";
  private int _damagesTaken;
  private const int MaxPerCombat = 3;

  public Action? OnMinionMadeGolden() => (Action) (() => this._damagesTaken = 0);

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() =>
    {
      if (this._damagesTaken >= 3)
        return;
      ++this._damagesTaken;
      int num = this.golden ? 2 : 1;
      if (this.ControlledByPlayer)
        this.Simulator.state.Player.BloodGemAtkBuff += num;
      else
        this.Simulator.state.Opponent.BloodGemAtkBuff += num;
    });
  }
}
