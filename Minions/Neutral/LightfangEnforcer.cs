// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.LightfangEnforcer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using HearthDb.Enums;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class LightfangEnforcer(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BGS_009";
  public const string Text = "At the end of your turn, give a friendly minion of each type +4/+4.";
  public const string GoldenText = "At the end of your turn, give a friendly minion of each type +8/+8.";
  private Race _primaryRace;

  public override Race PrimaryRace
  {
    get
    {
      return EnforcerPortrait.GetEnforcerRace((IEnumerable<Trinket>) this.FriendlyTrinkets, this._primaryRace);
    }
    set => this._primaryRace = value;
  }
}
