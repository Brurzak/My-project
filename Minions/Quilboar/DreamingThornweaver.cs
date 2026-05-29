// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.DreamingThornweaver
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class DreamingThornweaver(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG32_433";
  public const string Text = "<b>Avenge (4):</b> Your <b>Blood Gems</b> give an extra +1 Health this game. <i>(Swaps to Attack next turn!)</i>4[x]<b>Avenge (4):</b> Your <b>Blood Gems</b> give an extra +1 Attack this game. <i>(Swaps to Health next turn!)</i>";
  public const string GoldenText = "<b>Avenge (4):</b> Your <b>Blood Gems</b> give an extra +2 Health this game. <i>(Swaps to Attack next turn!)</i>4[x]<b>Avenge (4):</b> Your <b>Blood Gems</b> give an extra +2 Attack this game. <i>(Swaps to Health next turn!)</i>";

  public new int AvengeCounter { get; set; }

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(1);
      if (this.ControlledByPlayer)
        this.Simulator.state.Player.BloodGemHealthBuff += num;
      else
        this.Simulator.state.Opponent.BloodGemHealthBuff += num;
    });
  }
}
