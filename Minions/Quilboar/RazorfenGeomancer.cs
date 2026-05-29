// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.RazorfenGeomancer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class RazorfenGeomancer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG20_100";
  public const string Text = "<b>Battlecry:</b> Get 2 <b>Blood Gems</b>.";
  public const string GoldenText = "<b>Battlecry:</b> Get 4 <b>Blood Gems</b>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.AddBloodGemToFriendlyHand();
      this.AddBloodGemToFriendlyHand();
      if (!this.golden)
        return;
      this.AddBloodGemToFriendlyHand();
      this.AddBloodGemToFriendlyHand();
    });
  }
}
