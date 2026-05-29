// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.TavishAimLeftHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class TavishAimLeftHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnStartOfCombat, IEntity
{
  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.OpposingSide.Count == 0)
        return;
      this.Simulator.ProcessDamage(this.Data.Data2, this.OpposingSide.First<Minion>(), (Entity) this);
    });
  }
}
