// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.CatacombCrasher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class CatacombCrasher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionFailedToSummonNoSpace,
  IEntity
{
  public const string CardId = "BG30_129";
  public const string Text = "Whenever you would summon a minion that doesn't fit in your warband, give your minions +{0}/+{1} permanently.";
  public const string GoldenText = "Whenever you would summon a minion that doesn't fit in your warband, give your minions +{0}/+{1} permanently.";

  public Action? OnFriendlyMinionFailedToSummonNoSpace(Summon summoned)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(1));
    });
  }
}
