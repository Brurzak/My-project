// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.P0ultr0n
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class P0ultr0n(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG33_371";
  public const string Text = "<b>Avenge ({0}):</b> Gain <b>Divine Shield</b> and attack immediately.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Gain <b>Divine Shield</b> and attack immediately, twice.";

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1) && this.IsAlive(); ++index)
      {
        this.div = 1;
        this.Simulator.ResolveAllDeathsAndTriggers(true);
        this.Simulator.AttackWithMinion((Minion) this);
      }
    });
  }
}
