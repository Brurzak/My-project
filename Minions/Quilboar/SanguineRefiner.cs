// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.SanguineRefiner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class SanguineRefiner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_885";
  public const string Text = "<b>Rally:</b> Your <b>Blood Gems</b> give an extra +{0}/+{1} this game.";
  public const string GoldenText = "<b>Rally:</b> Your <b>Blood Gems</b> give an extra +{0}/+{1} this game.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num1 = isGolden ? 2 : 1;
      int num2 = isGolden ? 2 : 1;
      if (minion.ControlledByPlayer)
      {
        minion.Simulator.state.Player.BloodGemAtkBuff += num1;
        minion.Simulator.state.Player.BloodGemHealthBuff += num2;
      }
      else
      {
        minion.Simulator.state.Opponent.BloodGemAtkBuff += num1;
        minion.Simulator.state.Opponent.BloodGemHealthBuff += num2;
      }
    });
  }
}
