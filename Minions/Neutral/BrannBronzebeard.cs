// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BrannBronzebeard
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using HearthDb.Enums;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class BrannBronzebeard(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG_LOE_077";
  public const string Text = "Your <b>Battlecries</b> trigger twice.";
  public const string GoldenText = "Your <b>Battlecries</b> trigger three times.";
  private Race _primaryRace;
  private Race _secondaryRace;

  public override Race PrimaryRace
  {
    get
    {
      return BronzebeardPortrait.GetBronzebeardFirstRace((IEnumerable<Trinket>) this.FriendlyTrinkets, this._primaryRace);
    }
    set => this._primaryRace = value;
  }

  public override Race SecondaryRace
  {
    get
    {
      return BronzebeardPortrait.GetBronzebeardSecondRace((IEnumerable<Trinket>) this.FriendlyTrinkets, this._secondaryRace);
    }
    set => this._secondaryRace = value;
  }
}
