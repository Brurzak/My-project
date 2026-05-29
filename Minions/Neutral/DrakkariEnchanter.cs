// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.DrakkariEnchanter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using HearthDb.Enums;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class DrakkariEnchanter(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG26_ICC_901";
  public const string Text = "Your end of turn effects trigger twice.";
  public const string GoldenText = "Your end of turn effects trigger three times.";
  private Race _primaryRace;
  private Race _secondaryRace;

  public override Race PrimaryRace
  {
    get
    {
      return DrakkariPortrait.GetDrakkariFirstRace((IEnumerable<Trinket>) this.FriendlyTrinkets, this._primaryRace);
    }
    set => this._primaryRace = value;
  }

  public override Race SecondaryRace
  {
    get
    {
      return DrakkariPortrait.GetDrakkariSecondRace((IEnumerable<Trinket>) this.FriendlyTrinkets, this._secondaryRace);
    }
    set => this._secondaryRace = value;
  }
}
