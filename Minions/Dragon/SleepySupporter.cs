// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.SleepySupporter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class SleepySupporter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_241";
  public const string Text = "<b>Rally:</b> Give the minion to the right of this +{0}/+{1}.";
  public const string GoldenText = "<b>Rally:</b> Give the minion to the right of this +{0}/+{1}.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int attackBuff = isGolden ? 4 : 2;
      int healthBuff = isGolden ? 4 : 2;
      this.GetRightNeighbor()?.IncreaseStats(attackBuff, healthBuff);
    });
  }
}
