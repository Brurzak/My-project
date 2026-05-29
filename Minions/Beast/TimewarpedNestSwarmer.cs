// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedNestSwarmer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedNestSwarmer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle,
  IOnRally
{
  public const string CardId = "BG34_Giant_687";
  public const string Text = "<b>Battlecry, Deathrattle, and Rally:</b> Your Beetles have +{2}/+{3} this game. Summon a {0}/{1} Beetle.";
  public const string GoldenText = "<b>Battlecry, Deathrattle, and Rally:</b> Your Beetles have +{2}/+{3} this game. Summon two {0}/{1} Beetles.";

  private void TriggerEffect()
  {
    int num1 = this.DoubleIfGolden(2);
    int num2 = this.DoubleIfGolden(2);
    if (this.ControlledByPlayer)
    {
      this.Simulator.state.Player.BeetlesAtkBuff += num1;
      this.Simulator.state.Player.BeetlesHealthBuff += num2;
    }
    else
    {
      this.Simulator.state.Opponent.BeetlesAtkBuff += num1;
      this.Simulator.state.Opponent.BeetlesHealthBuff += num2;
    }
    this.TrySummonMinion(new Summon("BG28_603t_G", this.golden));
    if (!this.golden)
      return;
    this.TrySummonMinion(new Summon("BG28_603t_G", this.golden));
  }

  public Action? OnBattlecry() => (Action) (() => this.TriggerEffect());

  public Action<Minion> GetDeathrattle() => (Action<Minion>) (minion => this.TriggerEffect());

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion => this.TriggerEffect());
  }
}
