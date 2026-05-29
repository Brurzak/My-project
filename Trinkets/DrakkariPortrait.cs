// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.DrakkariPortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class DrakkariPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : Trinket(cardId, simulator, controlledByPlayer)
{
  public const string CardId = "BG32_MagicItem_179";

  public static Race GetDrakkariFirstRace(IEnumerable<Trinket> trinkets, Race defaultRace)
  {
    return !trinkets.Any<Trinket>((Func<Trinket, bool>) (x => x.CardID == "BG32_MagicItem_179")) ? defaultRace : (Race) 17;
  }

  public static Race GetDrakkariSecondRace(IEnumerable<Trinket> trinkets, Race defaultRace)
  {
    return !trinkets.Any<Trinket>((Func<Trinket, bool>) (x => x.CardID == "BG32_MagicItem_179")) ? defaultRace : (Race) 18;
  }
}
