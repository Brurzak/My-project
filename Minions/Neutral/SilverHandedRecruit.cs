// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.SilverHandedRecruit
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class SilverHandedRecruit(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_853";
  public const string Text = "<b>Battlecry:</b> Give your other even-Tier minions +4/+4.";
  public const string GoldenText = "<b>Battlecry:</b> Give your other even-Tier minions +8/+8.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m != this && m.IsAlive() && m.tier % 2 == 0)).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(4), this.DoubleIfGolden(4));
    });
  }
}
