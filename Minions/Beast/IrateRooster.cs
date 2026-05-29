// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.IrateRooster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class IrateRooster(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG29_990";
  public const string Text = "<b>Start of Combat:</b> Deal 1 damage to adjacent minions and give them +{0} Attack.";
  public const string GoldenText = "<b>Start of Combat:</b> Deal 1 damage to adjacent minions and give them +{0} Attack, twice.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion target in new List<Minion>()
      {
        this.GetLeftNeighbor(),
        this.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null && x.IsAlive())).Cast<Minion>().ToList<Minion>())
      {
        this.Simulator.ProcessDamage(this.DoubleIfGolden(1), target, (Entity) this);
        target.IncreaseStats(this.DoubleIfGolden(4), 0);
      }
    });
  }
}
