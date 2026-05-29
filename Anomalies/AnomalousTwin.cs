// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Anomalies.AnomalousTwin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Anomalies;

public class AnomalousTwin(string cardId, Simulator simulator) : Anomaly(cardId, simulator), IOnStartOfCombat, IEntity
{
  public const string CardId = "BG27_Anomaly_560";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion highestHealthMinion1 = this.Simulator.GetHighestHealthMinion(this.Simulator.playerSide);
      if (highestHealthMinion1 != null)
        this.Simulator.TrySummonMinion((Summon) highestHealthMinion1.Clone(), this.Simulator.playerSide, this.Simulator.playerSide.Count, (Entity) this);
      Minion highestHealthMinion2 = this.Simulator.GetHighestHealthMinion(this.Simulator.opponentSide);
      if (highestHealthMinion2 == null)
        return;
      this.Simulator.TrySummonMinion((Summon) highestHealthMinion2.Clone(), this.Simulator.opponentSide, this.Simulator.opponentSide.Count, (Entity) this);
    });
  }
}
