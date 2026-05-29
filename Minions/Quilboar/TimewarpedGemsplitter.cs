// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedGemsplitter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedGemsplitter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionLostDiv,
  IEntity
{
  public const string CardId = "BG34_Giant_644";
  public const string Text = "<b>Divine Shield</b>. After a friendly minion loses <b>Divine Shield</b>, your <b>Blood Gems</b> give an extra +{0} Attack this game.";
  public const string GoldenText = "<b>Divine Shield</b>. After a friendly minion loses <b>Divine Shield</b>, your <b>Blood Gems</b> give an extra +{0} Attack this game.";

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      if (this.ControlledByPlayer)
        this.Simulator.state.Player.BloodGemAtkBuff += num;
      else
        this.Simulator.state.Opponent.BloodGemAtkBuff += num;
    });
  }
}
