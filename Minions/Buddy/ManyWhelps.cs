// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.ManyWhelps
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class ManyWhelps(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG22_HERO_305_Buddy";
  public const string Text = "Whenever you summon a Whelp, gain +2/+2 permanently.";
  public const string GoldenText = "Whenever you summon a Whelp, gain +4/+4 permanently.";

  public Action? OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return !this.IsWhelp(summoned.CardID) ? (Action) null : (Action) (() => this.IncreaseStats(this.golden ? 4 : 2));
  }

  private bool IsWhelp(string cardId) => cardId == "BG22_HERO_305t" || cardId == "BG24_300";
}
