// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.WhelpSmuggler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using HearthDb.Enums;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class WhelpSmuggler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG21_013";
  public const string Text = "Whenever a friendly Dragon gains Attack, give it +1 Health.";
  public const string GoldenText = "Whenever a friendly Dragon gains Attack, give it +2 Health.";
  private Race _primaryRace;

  public override Race PrimaryRace
  {
    get
    {
      return SmugglerPortrait.GetWhelpRace((IEnumerable<Trinket>) this.FriendlyTrinkets, this._primaryRace);
    }
    set => this._primaryRace = value;
  }

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (attackChange <= 0 || !buffed.IsDragon() || !buffed.IsAlive())
        return;
      buffed.IncreaseStats(0, this.DoubleIfGolden(1));
    });
  }
}
