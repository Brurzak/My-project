// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TimewarpedJellyBelly
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TimewarpedJellyBelly(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionReborn,
  IEntity
{
  public const string CardId = "BG34_Giant_024";
  public const string Text = "After a friendly minion is <b>Reborn</b>, give your minions +{0}/+{1} permanently.";
  public const string GoldenText = "After a friendly minion is <b>Reborn</b>, give your minions +{0}/+{1} permanently.";

  public Action? OnAfterFriendlyMinionReborn(Minion minion)
  {
    return (Action) (() =>
    {
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>())
        minion1.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(2));
    });
  }
}
