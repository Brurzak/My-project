// Decompiled with JetBrains decompiler
// Type: BobsBuddy.BloodGem
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy;

public class BloodGem(Entity? source, Simulator simulator) : CardEntity("BG20_GEM", source, simulator)
{
  public override CardEntity Clone(Entity? source, Simulator? simulator = null)
  {
    return (CardEntity) new BloodGem(source, simulator ?? this.simulator);
  }

  public override CardEntity Copy(Entity? source, Simulator? simulator = null)
  {
    BloodGem bloodGem = new BloodGem(source, simulator ?? this.simulator);
    bloodGem.Original = this.Original;
    return (CardEntity) bloodGem;
  }

  public override string ToString() => nameof (BloodGem);
}
