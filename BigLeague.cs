// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Anomalies.BigLeague
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Anomalies;

public class BigLeague(string cardId, Simulator simulator) : Anomaly(cardId, simulator), IAvailableTiersOverride
{
  public const string CardId = "BG27_Anomaly_100";

  public HashSet<int> AvailableTiersOverride { get; } = new HashSet<int>()
  {
    3,
    4,
    5,
    6
  };
}
