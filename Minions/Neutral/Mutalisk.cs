// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.Mutalisk
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class Mutalisk(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG31_HERO_811t6";
  public const string Text = "After a friendly minion kills an enemy, gain +3 Attack permanently. <i>(Morphs each turn!)</i>[x]After a friendly minion kills an enemy, gain +3 Attack permanently.";
  public const string GoldenText = "After a friendly minion kills an enemy, gain +6 Attack permanently. <i>(Morphs each turn!)</i>[x]After a friendly minion kills an enemy, gain +6 Attack permanently.";

  public Action OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() => this.IncreaseStats(this.golden ? 6 : 3, 0));
  }
}
