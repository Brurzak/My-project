// Decompiled with JetBrains decompiler
// Type: BobsBuddy.MinionCardEntity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy;

public class MinionCardEntity : CardEntity
{
  public Minion Data { get; }

  public bool CanSummon { get; set; } = true;

  public MinionCardEntity(Minion minion, Entity? source, Simulator simulator)
    : base(minion.CardID, source, simulator)
  {
    this.Data = minion;
  }

  public override CardEntity Clone(Entity? source, Simulator? simulator = null)
  {
    return (CardEntity) new MinionCardEntity(this.Data.Clone(simulator), source, simulator ?? this.simulator)
    {
      CanSummon = this.CanSummon
    };
  }

  public override CardEntity Copy(Entity? source, Simulator? simulator = null)
  {
    MinionCardEntity minionCardEntity = new MinionCardEntity(this.Data.Clone(simulator), source, simulator ?? this.simulator);
    minionCardEntity.Original = this.Original;
    minionCardEntity.CanSummon = this.CanSummon;
    return (CardEntity) minionCardEntity;
  }

  public override string ToString()
  {
    return this.Data.ToString() + (this == this.Original ? "" : " (copy)");
  }
}
