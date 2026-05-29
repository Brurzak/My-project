// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.SanguineChampion
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class SanguineChampion(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IBattlecry
{
  public const string CardId = "BG23_017";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Your <b>Blood Gems</b> give an extra +1/+1 this game.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Your <b>Blood Gems</b> give an extra +2/+2 this game.";

  public Action<Minion> GetDeathrattle() => SanguineChampion.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      if (minion.ControlledByPlayer)
      {
        minion.Simulator.state.Player.BloodGemAtkBuff += num;
        minion.Simulator.state.Player.BloodGemHealthBuff += num;
      }
      else
      {
        minion.Simulator.state.Opponent.BloodGemAtkBuff += num;
        minion.Simulator.state.Opponent.BloodGemHealthBuff += num;
      }
    });
  }

  public Action? OnBattlecry()
  {
    return (Action) (() => SanguineChampion.Deathrattle(this.golden)((Minion) this));
  }
}
