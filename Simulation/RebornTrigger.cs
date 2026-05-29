// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.RebornTrigger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Simulation;

public class RebornTrigger
{
  public Func<IEnumerable<Minion>> Action { get; }

  public Entity? Source { get; }

  public bool Resolved { get; private set; }

  public RebornTrigger(Func<IEnumerable<Minion>> action, Entity? source)
  {
    this.Action = action;
    this.Source = source;
  }

  public IEnumerable<Minion> Invoke()
  {
    this.Resolved = true;
    return this.Action();
  }
}
