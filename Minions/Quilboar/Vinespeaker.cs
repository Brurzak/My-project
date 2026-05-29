// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.Vinespeaker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class Vinespeaker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG35_437";
  public const string Text = "After a friendly <b>Deathrattle</b> minion dies, your <b>Blood Gems</b> give an extra +{0} Attack this game.";
  public const string GoldenText = "After a friendly <b>Deathrattle</b> minion dies, your <b>Blood Gems</b> give an extra +{0} Attack this game.";

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.HasDeathrattle())
        return;
      int num = this.DoubleIfGolden(1);
      if (this.ControlledByPlayer)
        this.Simulator.state.Player.BloodGemAtkBuff += num;
      else
        this.Simulator.state.Opponent.BloodGemAtkBuff += num;
    });
  }
}
