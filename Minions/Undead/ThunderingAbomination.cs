// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.ThunderingAbomination
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class ThunderingAbomination(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity,
  IOnFriendlyMinionFailedToSummonNoSpace
{
  public const string CardId = "BG30_124";
  public const string Text = "Whenever you summon a minion in combat, give it +3/+3. If there's no space, gain +3/+3 permanently.";
  public const string GoldenText = "Whenever you summon a minion in combat, give it +6/+6. If there's no space, gain +6/+6 permanently.";

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() => summoned.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(3)));
  }

  public Action? OnFriendlyMinionFailedToSummonNoSpace(Summon summoned)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(3), this.DoubleIfGolden(3)));
  }
}
