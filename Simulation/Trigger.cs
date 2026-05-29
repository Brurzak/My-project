// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Trigger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;

#nullable enable
namespace BobsBuddy.Simulation;

public class Trigger
{
  public Action Action { get; }

  public IEntity? Source { get; }

  public bool Resolved { get; private set; }

  public bool IsDeathrattle { get; }

  public Trigger(Action action, IEntity? source, bool isDeathrattle)
  {
    this.Action = action;
    this.Source = source;
    this.IsDeathrattle = isDeathrattle;
  }

  public void Invoke()
  {
    this.Resolved = true;
    this.Action();
  }

  public void Ignore() => this.Resolved = true;

  public static implicit operator Trigger?((Action? action, IEntity? source) tuple)
  {
    return tuple.action == null ? (Trigger) null : new Trigger(tuple.action, tuple.source, false);
  }
}
