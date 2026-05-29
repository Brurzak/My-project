// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedGeomancer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedGeomancer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_Giant_305";
  public const string Text = "<b>Avenge ({0}):</b> Get a <b>Blood Gem</b>. Your <b>Blood Gems</b> give an extra +1/+1 this game.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get a <b>Blood Gem</b>. Your <b>Blood Gems</b> give an extra +2/+2 this game.";

  public int AvengeRequirement => 5;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      this.AddBloodGemToFriendlyHand();
      int num = this.DoubleIfGolden(1);
      if (this.ControlledByPlayer)
      {
        this.Simulator.state.Player.BloodGemAtkBuff += num;
        this.Simulator.state.Player.BloodGemHealthBuff += num;
      }
      else
      {
        this.Simulator.state.Opponent.BloodGemAtkBuff += num;
        this.Simulator.state.Opponent.BloodGemHealthBuff += num;
      }
    });
  }
}
