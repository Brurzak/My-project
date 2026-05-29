// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.WanderingWight
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class WanderingWight(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG31_126";
  public const string Text = "After you summon a minion in combat, give it Health equal to its Attack.";
  public const string GoldenText = "After you summon a minion in combat, give it Health equal to double its Attack.";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() => summoned.IncreaseStats(0, this.DoubleIfGolden(summoned.attack())));
  }
}
