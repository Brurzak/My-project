// Decompiled with JetBrains decompiler
// Type: BobsBuddy.CardEntity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System.Runtime.Serialization;

#nullable enable
namespace BobsBuddy;

public class CardEntity
{
  public readonly Simulator simulator;

  public string Id { get; }

  [IgnoreDataMember]
  public CardEntity Original { get; protected set; }

  public Entity? Source { get; }

  public CardEntity(string id, Entity? source, Simulator simulator)
  {
    this.Id = id;
    this.Source = source;
    this.Original = this;
    this.simulator = simulator;
  }

  public virtual CardEntity Clone(Entity? source, Simulator? simulator = null)
  {
    return new CardEntity(this.Id, source, simulator ?? this.simulator);
  }

  public virtual CardEntity Copy(Entity? source, Simulator? simulator = null)
  {
    return new CardEntity(this.Id, source, simulator ?? this.simulator)
    {
      Original = this.Original
    };
  }

  public override string ToString() => this.Id + (this == this.Original ? "" : " (copy)");
}
