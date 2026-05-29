// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SilkyShimmermoth
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SilkyShimmermoth(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionTakeDamage,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG32_204";
  public const string Text = "Whenever this takes damage, your Beetles have +{2}/+{3} this game. <b>Deathrattle:</b> Summon a {0}/{1} Beetle.";
  public const string GoldenText = "Whenever this takes damage, your Beetles have +{2}/+{3} this game. <b>Deathrattle:</b> Summon two {0}/{1} Beetles.";

  public Action? OnFriendlyMinionTakeDamage(Minion target, int value)
  {
    return (Action) (() =>
    {
      if (target != this)
        return;
      int num1 = this.DoubleIfGolden(2);
      int num2 = this.DoubleIfGolden(1);
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
    });
  }

  public Action<Minion> GetDeathrattle() => SilkyShimmermoth.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinion((Summon) "BG28_603t");
      if (!golden)
        return;
      minion.TrySummonMinion((Summon) "BG28_603t");
    });
  }
}
