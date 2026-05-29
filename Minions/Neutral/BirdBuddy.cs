// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BirdBuddy
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class BirdBuddy(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG21_002";
  public const string Text = "<b>Avenge (1):</b> Give your Beasts +1/+1.";
  public const string GoldenText = "<b>Avenge (1):</b> Give your Beasts +2/+2.";

  public int AvengeRequirement => 1;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsBeast() && x.IsAlive())))
        minion.IncreaseStats(this.DoubleIfGolden(1));
    });
  }
}
