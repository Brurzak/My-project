// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.EnforcerPortrait
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

public class EnforcerPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : Trinket(cardId, simulator, controlledByPlayer)
{
  public const string CardId = "BG30_MagicItem_971";

  public static Race GetEnforcerRace(IEnumerable<Trinket> trinkets, Race defaultRace)
  {
    return !trinkets.Any<Trinket>((Func<Trinket, bool>) (x => x.CardID == "BG30_MagicItem_971")) ? defaultRace : (Race) 26;
  }
}
