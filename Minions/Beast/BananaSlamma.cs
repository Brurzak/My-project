// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.BananaSlamma
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class BananaSlamma(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG26_802";
  public const string Text = "After you summon a Beast in combat, double its Attack.";
  public const string GoldenText = "After you summon a Beast in combat, triple its Attack.";

  public override Action? OnMinionSummonedByFriendly(
    Minion summoned,
    Entity? source,
    bool wasReborn)
  {
    return !summoned.IsBeast() || summoned.IsDead() ? (Action) null : (Action) (() =>
    {
      int num1 = 0;
      GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier != null)
        num1 = globalModifier.PassiveAttackBonusFor(summoned);
      if (summoned.CardID == "BG28_603t")
        num1 = (summoned.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent).BeetlesAtkBuff;
      int num2 = this.DoubleIfGolden(1);
      summoned.IncreaseStats(summoned.attack() * num2 + num1, 0);
    });
  }
}
