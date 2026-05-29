// Decompiled with JetBrains decompiler
// Type: BobsBuddy.RandomMinionCardEntity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy;

public class RandomMinionCardEntity(Entity? source, Simulator simulator) : CardEntity("Random Minion", source, simulator)
{
  public override CardEntity Clone(Entity? source, Simulator? simulator = null)
  {
    return (CardEntity) new RandomMinionCardEntity(source, simulator ?? this.simulator);
  }

  public override CardEntity Copy(Entity? source, Simulator? simulator = null)
  {
    RandomMinionCardEntity minionCardEntity = new RandomMinionCardEntity(source, simulator ?? this.simulator);
    minionCardEntity.Original = this.Original;
    return (CardEntity) minionCardEntity;
  }

  public override string ToString() => nameof (RandomMinionCardEntity);
}
