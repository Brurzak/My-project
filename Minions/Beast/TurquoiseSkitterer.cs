// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TurquoiseSkitterer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TurquoiseSkitterer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG31_809";
  public const string Text = "<b>Deathrattle:</b> Your Beetles have +{2}/+{3} this game. Summon a {0}/{1} Beetle.";
  public const string GoldenText = "<b>Deathrattle:</b> Your Beetles have +{2}/+{3} this game. Summon two {0}/{1} Beetles.";

  public Action<Minion> GetDeathrattle() => TurquoiseSkitterer.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      GameState.PlayerState playerState = minion.ControlledByPlayer ? minion.Simulator.state.Player : minion.Simulator.state.Opponent;
      playerState.BeetlesAtkBuff += golden ? 10 : 5;
      playerState.BeetlesHealthBuff += golden ? 10 : 5;
      minion.TrySummonMinion((Summon) "BG28_603t");
      if (!golden)
        return;
      minion.TrySummonMinion((Summon) "BG28_603t");
    });
  }
}
