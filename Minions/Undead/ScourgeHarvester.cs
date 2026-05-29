// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.ScourgeHarvester
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class ScourgeHarvester(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG33_114";
  public const string Text = "After you summon an Undead in combat, gain its stats.";
  public const string GoldenText = "After you summon an Undead in combat, gain double its stats.";

  public Action OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsUndead())
        return;
      this.IncreaseStats(this.DoubleIfGolden(summoned.attack()), this.DoubleIfGolden(summoned.health()));
    });
  }
}
