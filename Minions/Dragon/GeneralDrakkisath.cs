// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.GeneralDrakkisath
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class GeneralDrakkisath(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_309";
  public const string Text = "<b>Battlecry:</b> Get a 2/1 Smolderwing with \"<b>Battlecry:</b> Give a Dragon +5 Attack.\"";
  public const string GoldenText = "<b>Battlecry:</b> Get two 2/1 Smolderwings with \"<b>Battlecry:</b> Give a Dragon +5 Attack.\"";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.AddMinionToFriendlyHand("BG25_309t");
      if (!this.golden)
        return;
      this.AddMinionToFriendlyHand("BG25_309t");
    });
  }
}
