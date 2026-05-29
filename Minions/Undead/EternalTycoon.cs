// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.EternalTycoon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class EternalTycoon(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_403";
  public const string Text = "<b>Avenge ({0}):</b> Summon an Eternal Knight. It attacks immediately.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Summon a Golden Eternal Knight. It attacks immediately.";

  public int AvengeRequirement => 5;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      List<Minion> minionList = this.TrySummonMinion(new Summon("BG25_008", this.golden));
      if (minionList.Count <= 0)
        return;
      using (this.Simulator.state.TriggerScope.New(nameof (EternalTycoon)))
        this.Simulator.AttackWithMinion(minionList[0]);
    });
  }
}
