// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.PricklyPiper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class PricklyPiper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_160";
  public const string Text = "<b>Deathrattle:</b> Your <b>Blood Gems</b> give an extra +1 Attack this game.";
  public const string GoldenText = "<b>Deathrattle:</b> Your <b>Blood Gems</b> give an extra +2 Attack this game.";

  public Action<Minion> GetDeathrattle() => PricklyPiper.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      if (minion.ControlledByPlayer)
        minion.Simulator.state.Player.BloodGemAtkBuff += num;
      else
        minion.Simulator.state.Opponent.BloodGemAtkBuff += num;
    });
  }
}
