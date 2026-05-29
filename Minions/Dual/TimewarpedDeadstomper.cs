// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.TimewarpedDeadstomper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class TimewarpedDeadstomper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG34_Giant_654";
  public const string Text = "After you summon a minion, give your minions +{0} Attack permanently.";
  public const string GoldenText = "After you summon a minion, give your minions +{0} Attack permanently.";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide)
        minion.IncreaseStats(this.DoubleIfGolden(4), 0, (Entity) this);
    });
  }
}
