// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Factory.AnomalyFactory
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Anomalies;
using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Factory;

public class AnomalyFactory(Simulator simulator) : EntityFactory<Anomaly>(simulator)
{
  public Anomaly Create(string cardId)
  {
    EntityFactory<Anomaly>.Constructor constructor;
    if (!EntityFactory<Anomaly>.Constructors.TryGetValue(cardId, out constructor))
      return new Anomaly(cardId, this._simulator);
    return constructor((object) cardId, (object) this._simulator);
  }
}
