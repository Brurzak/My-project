// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.TimewarpedPagle
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class TimewarpedPagle(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG34_Giant_208";
  public const string Text = "Once per combat, when this attacks and kills a minion, get a Triple Reward.";
  public const string GoldenText = "Once per combat, when this attacks and kills a minion, get 2 Triple Rewards.";
  private const int MaxTriggers = 1;
  private int _triggerCounter;

  public Action? OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() =>
    {
      if (friendly != this || this._triggerCounter >= 1)
        return;
      this.AddSpellToFriendlyHand();
      if (this.golden)
        this.AddSpellToFriendlyHand();
      ++this._triggerCounter;
    });
  }
}
