// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.InspiringUnderdog
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class InspiringUnderdog(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG30_127";
  public const string Text = "<b>Battlecry:</b> Give your minions from Tier 3 or lower +3/+1.";
  public const string GoldenText = "<b>Battlecry:</b> Give your minions from Tier 3 or lower +6/+2.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive() && m.tier <= 3)).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(1));
    });
  }
}
