// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedHyena
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedHyena(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG34_Giant_581";
  public const string Text = "Whenever a friendly Beast dies, gain +{0}/+{1} permanently.";
  public const string GoldenText = "Whenever a friendly Beast dies, gain +{0}/+{1} permanently.";

  public Action OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsBeast())
        return;
      this.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(2));
    });
  }
}
