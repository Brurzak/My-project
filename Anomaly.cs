// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Anomalies.Anomaly
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Anomalies;

public class Anomaly(string cardId, Simulator simulator) : Entity(cardId, simulator, true)
{
  public Anomaly Clone(Simulator? simulator = null)
  {
    Anomaly anomaly = (simulator ?? this.Simulator).AnomalyFactory.Create(this.CardID);
    anomaly.CloneFromBaseEntity((Entity) this);
    return anomaly;
  }
}
