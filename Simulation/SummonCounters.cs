// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.SummonCounters
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public class SummonCounters
{
  public Dictionary<int, int> Player { get; } = new Dictionary<int, int>();

  public Dictionary<int, int> Opponent { get; } = new Dictionary<int, int>();
}
