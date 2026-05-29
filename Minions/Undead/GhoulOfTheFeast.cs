// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.GhoulOfTheFeast
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class GhoulOfTheFeast(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG25_002";
  public const string Text = "<b>Avenge ({2}):</b> Give a friendly minion of each type +{0}/+{1} permanently.";
  public const string GoldenText = "<b>Avenge ({2}):</b> Give a friendly minion of each type +{0}/+{1} permanently.";

  public int AvengeRequirement => 1;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.GetRandomPerRace())
        minion.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(2));
    });
  }
}
