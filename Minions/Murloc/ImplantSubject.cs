// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.ImplantSubject
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class ImplantSubject(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionLostBonusKeyword,
  IOnFriendlyMinionLostDiv,
  IEntity,
  IOnFriendlyMinionLostVenomous,
  IOnFriendlyMinionLostStealth,
  IOnFriendlyMinionLostTaunt,
  IOnFriendlyMinionLostReborn,
  IOnFriendlyMinionLostWindfury
{
  public const string CardId = "BG31_147";
  public const string Text = "<b>Stealth</b>. Whenever a friendly minion loses a <b>Bonus Keyword</b> in combat, gain +1/+1 permanently.";
  public const string GoldenText = "<b>Stealth</b>. Whenever a friendly minion loses a <b>Bonus Keyword</b> in combat, gain +2/+2 permanently.";

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1)));
  }

  public Action? OnFriendlyMinionLostVenomous(Minion minion)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1)));
  }

  public Action? OnFriendlyMinionLostStealth(Minion lost)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1)));
  }

  public Action? OnFriendlyMinionLostTaunt(Minion lost)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1)));
  }

  public Action? OnFriendlyMinionLostReborn(Minion lost)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1)));
  }

  public Action? OnFriendlyMinionLostWindfury(Minion lost)
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1)));
  }
}
