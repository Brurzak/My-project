// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Anomalies.BlessedOrBlighted
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Anomalies;

public class BlessedOrBlighted(string cardId, Simulator simulator) : 
  Anomaly(cardId, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG27_Anomaly_726";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion first1;
      Minion last1;
      if (this.Simulator.playerSide.TryGetFirstAndLast<Minion>(out first1, out last1))
      {
        first1.div = 1;
        last1.reborn = true;
      }
      Minion first2;
      Minion last2;
      if (!this.Simulator.opponentSide.TryGetFirstAndLast<Minion>(out first2, out last2))
        return;
      first2.div = 1;
      last2.reborn = true;
    });
  }
}
