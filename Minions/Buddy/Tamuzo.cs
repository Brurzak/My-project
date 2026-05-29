// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Tamuzo
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Tamuzo(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG23_HERO_201_Buddy";
  public const string Text = "After you summon a minion in combat, double its stats.";
  public const string GoldenText = "After you summon a minion in combat, triple its stats.";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      int num1 = 0;
      int num2 = 0;
      GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier != null)
      {
        num1 = globalModifier.PassiveAttackBonusFor(summoned);
        num2 = globalModifier.PassiveHealthBonusFor(summoned);
      }
      if (summoned.CardID == "BG28_603t")
      {
        GameState.PlayerState playerState = summoned.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent;
        num1 = playerState.BeetlesAtkBuff;
        num2 = playerState.BeetlesHealthBuff;
      }
      int num3 = this.DoubleIfGolden(1);
      summoned.IncreaseStats(summoned.attack() * num3 + num1, summoned.health() * num3 + num2);
    });
  }
}
