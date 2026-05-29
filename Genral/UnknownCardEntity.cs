// Decompiled with JetBrains decompiler
// Type: BobsBuddy.UnknownCardEntity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy;

public class UnknownCardEntity(Entity? source, Simulator simulator) : CardEntity("Unknown", source, simulator)
{
  public override CardEntity Clone(Entity? source, Simulator? simulator = null)
  {
    return (CardEntity) new UnknownCardEntity(source, simulator ?? this.simulator);
  }

  public override CardEntity Copy(Entity? source, Simulator? simulator = null)
  {
    UnknownCardEntity unknownCardEntity = new UnknownCardEntity(source, simulator ?? this.simulator);
    unknownCardEntity.Original = this.Original;
    return (CardEntity) unknownCardEntity;
  }

  public override string ToString() => nameof (UnknownCardEntity);
}
