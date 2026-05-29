// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.Scope`1
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;

#nullable enable
namespace BobsBuddy.Simulation;

public class Scope<T> : IDisposable where T : new()
{
  public Scope(string? name) => this.Name = name;

  public string? Name { get; }

  public T Data => this.Current.Data;

  public Scope<
  #nullable disable
  T>.ScopeData Current { get; protected set; } = new Scope<T>.ScopeData("default", (Scope<T>.ScopeData) null);

  public 
  #nullable enable
  Scope<T> New(string name)
  {
    this.Current = new Scope<T>.ScopeData(name, this.Current);
    return this;
  }

  public void Dispose()
  {
    if (this.Current.Parent == null)
      return;
    this.Current = this.Current.Parent;
  }

  public class ScopeData
  {
    public ScopeData(string name, Scope<
    #nullable disable
    T>.ScopeData
    #nullable enable
    ? parent)
    {
      this.Name = name;
      this.Parent = parent;
    }

    public string Name { get; }

    public T Data { get; } = new T();

    public Scope<
    #nullable disable
    T>.ScopeData
    #nullable enable
    ? Parent { get; }
  }
}
