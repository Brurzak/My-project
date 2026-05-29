// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.JellyBelly
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class JellyBelly(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionReborn,
  IEntity
{
  public const string CardId = "BG25_005";
  public const string Text = "After a friendly minion is <b>Reborn</b>, gain +2/+3 permanently.";
  public const string GoldenText = "After a friendly minion is <b>Reborn</b>, gain +4/+6 permanently.";

  public Action? OnAfterFriendlyMinionReborn(Minion minion)
  {
    return (Action) (() => this.AttachedOrThis.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(3)));
  }
}
