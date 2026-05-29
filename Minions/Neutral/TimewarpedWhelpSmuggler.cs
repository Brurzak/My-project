// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TimewarpedWhelpSmuggler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class TimewarpedWhelpSmuggler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG34_Giant_064";
  public const string Text = "Whenever a friendly minion gains Attack, give it +{1} Health.";
  public const string GoldenText = "Whenever a friendly minion gains Attack, give it +{1} Health.";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (attackChange <= 0 || buffed.IsDead())
        return;
      buffed.IncreaseStats(0, this.DoubleIfGolden(3));
    });
  }
}
