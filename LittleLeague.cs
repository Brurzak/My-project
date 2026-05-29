// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Anomalies.LittleLeague
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Anomalies;

public class LittleLeague(string cardId, Simulator simulator) : Anomaly(cardId, simulator), IAvailableTiersOverride
{
  public const string CardId = "BG27_Anomaly_800";

  public HashSet<int> AvailableTiersOverride { get; } = new HashSet<int>()
  {
    1,
    2,
    3,
    4
  };
}
