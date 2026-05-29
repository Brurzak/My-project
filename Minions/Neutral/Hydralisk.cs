// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Hydralisk
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class Hydralisk(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG31_HERO_811t4";
  public const string Text = "<b>Windfury</b> <b>Rally:</b> Gain Attack equal to your Tier permanently. <i>(Morphs each turn!)</i>[x]<b>Windfury</b> <b>Rally:</b> Gain Attack equal to your Tier permanently.";
  public const string GoldenText = "<b>Windfury</b> <b>Rally:</b> Gain Attack equal to twice your Tier permanently. <i>(Morphs each turn!)</i>[x]<b>Windfury</b> <b>Rally:</b> Gain Attack equal to twice your Tier permanently.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = minion.ControlledByPlayer ? minion.Simulator.PlayerInput.Tier : minion.Simulator.OpponentInput.Tier;
      int attackBuff = isGolden ? num * 2 : num;
      minion.IncreaseStats(attackBuff, 0);
    });
  }
}
