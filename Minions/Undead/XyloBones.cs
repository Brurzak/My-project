// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.XyloBones
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class XyloBones(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG26_172";
  public const string Text = "After you summon a minion in combat, gain +2 Health permanently.";
  public const string GoldenText = "After you summon a minion in combat, gain +4 Health permanently.";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() => this.IncreaseStats(0, this.DoubleIfGolden(2)));
  }
}
