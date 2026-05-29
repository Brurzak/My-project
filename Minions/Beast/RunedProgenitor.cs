// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.RunedProgenitor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class RunedProgenitor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IAvenge
{
  public const string CardId = "BG31_808";
  public const string Text = "<b>Avenge (3):</b> Your Beetles have +{2}/+{3} this game. <b>Deathrattle:</b> Summon a {0}/{1} Beetle.";
  public const string GoldenText = "<b>Avenge (3):</b> Your Beetles have +{2}/+{3} this game. <b>Deathrattle:</b> Summon two {0}/{1} Beetles.";

  public Action<Minion> GetDeathrattle() => RunedProgenitor.Deathrattle(this.golden);

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

  public int AvengeRequirement => 3;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      if (this.ControlledByPlayer)
      {
        this.Simulator.state.Player.BeetlesAtkBuff += this.golden ? 6 : 3;
        this.Simulator.state.Player.BeetlesHealthBuff += this.golden ? 4 : 2;
      }
      else
      {
        this.Simulator.state.Opponent.BeetlesAtkBuff += this.golden ? 6 : 3;
        this.Simulator.state.Opponent.BeetlesHealthBuff += this.golden ? 4 : 2;
      }
    });
  }
}
