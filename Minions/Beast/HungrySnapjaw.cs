// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.HungrySnapjaw
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class HungrySnapjaw(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG26_370";
  public const string Text = "After a friendly Beast dies, gain +1 Health permanently.";
  public const string GoldenText = "After a friendly Beast dies, gain +2 Health permanently.";

  public Action OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsBeast())
        return;
      this.IncreaseStats(0, this.DoubleIfGolden(1));
    });
  }
}
