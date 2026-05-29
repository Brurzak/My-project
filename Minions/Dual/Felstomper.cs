// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.Felstomper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class Felstomper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG25_042";
  public const string Text = "After you summon a minion in combat, give your minions +3 Attack.";
  public const string GoldenText = "After you summon a minion in combat, give your minions +6 Attack.";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide)
        minion.IncreaseStats(this.DoubleIfGolden(3), 0, (Entity) this);
    });
  }
}
