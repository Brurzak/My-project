// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.TimewarpedKarathress
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class TimewarpedKarathress(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG34_PreMadeChamp_056";
  public const string Text = "After you summon a minion in combat, get a copy of Deep Blues.";
  public const string GoldenText = "After you summon a minion in combat, get 2 copies of Deep Blues.";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      this.AddSpellToFriendlyHand();
      if (!this.golden)
        return;
      this.AddSpellToFriendlyHand();
    });
  }
}
