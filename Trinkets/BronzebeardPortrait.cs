// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BronzebeardPortrait
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

public class BronzebeardPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer)
{
  public const string CardId = "BG30_MagicItem_418";

  public static Race GetBronzebeardFirstRace(IEnumerable<Trinket> trinkets, Race defaultRace)
  {
    return !trinkets.Any<Trinket>((Func<Trinket, bool>) (x => x.CardID == "BG30_MagicItem_418")) ? defaultRace : (Race) 24;
  }

  public static Race GetBronzebeardSecondRace(IEnumerable<Trinket> trinkets, Race defaultRace)
  {
    return !trinkets.Any<Trinket>((Func<Trinket, bool>) (x => x.CardID == "BG30_MagicItem_418")) ? defaultRace : (Race) 14;
  }
}
