// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.CarbonicCopy
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class CarbonicCopy(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG27_503";
  public const string Text = "<b>Start of Combat:</b> Summon a copy of this minion.";
  public const string GoldenText = "<b>Start of Combat:</b> Summon two copies of this minion.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      this.TrySummonMinion((Summon) this.Clone());
      if (!this.golden)
        return;
      this.TrySummonMinion((Summon) this.Clone());
    });
  }
}
