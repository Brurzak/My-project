// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.MoonBaconJazzer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class MoonBaconJazzer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG26_159";
  public const string Text = "<b>Battlecry:</b> Your <b>Blood Gems</b> give an extra +1 Health this game.";
  public const string GoldenText = "<b>Battlecry:</b> Your <b>Blood Gems</b> give an extra +2 Health this game.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      if (this.ControlledByPlayer)
        this.Simulator.state.Player.BloodGemHealthBuff += num;
      else
        this.Simulator.state.Opponent.BloodGemHealthBuff += num;
    });
  }
}
