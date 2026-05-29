// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.ScavengingHyena
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class ScavengingHyena(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG_EX1_531";
  public const string Text = "Whenever a friendly Beast dies, gain +2/+1.";
  public const string GoldenText = "Whenever a friendly Beast dies, gain +4/+2.";

  public Action OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsBeast())
        return;
      this.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(1));
    });
  }
}
