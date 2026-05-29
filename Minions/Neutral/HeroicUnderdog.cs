// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.HeroicUnderdog
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class HeroicUnderdog(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_604";
  public const string Text = "<b>Stealth</b> <b>Rally:</b> Gain the target's Attack.";
  public const string GoldenText = "<b>Stealth</b> <b>Rally:</b> Gain double the target's Attack.";

  public Action<Minion> OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = isGolden ? 2 : 1;
      minion.IncreaseStats(target.attack() * num, 0);
    });
  }
}
