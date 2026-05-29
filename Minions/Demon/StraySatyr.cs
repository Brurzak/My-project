// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.StraySatyr
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class StraySatyr(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDealsDamage,
  IEntity
{
  public const string CardId = "BG33_151";
  public const string Text = "After a friendly Demon deals damage, gain +{0} Attack.";
  public const string GoldenText = "After a friendly Demon deals damage, gain +{0} Attack.";

  public Action? OnFriendlyMinionDealsDamage(Minion source, Minion target, int value)
  {
    return (Action) (() =>
    {
      if (!source.IsDemon())
        return;
      this.IncreaseStats(this.DoubleIfGolden(1), 0);
    });
  }
}
