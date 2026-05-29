// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.StrongshellScavenger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class StrongshellScavenger(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG_ICC_807";
  public const string Text = "<b>Battlecry:</b> Give your <b>Taunt</b> minions +2/+2.";
  public const string GoldenText = "<b>Battlecry:</b> Give your <b>Taunt</b> minions +4/+4.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(2);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.taunt && x.IsAlive())).ToList<Minion>())
        minion.IncreaseStats(num, num);
    });
  }
}
