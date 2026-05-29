// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.EclipsionIllidari
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class EclipsionIllidari(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_08_Buddy";
  public const string Text = "Your first minion that attacks has \"<b>Immune</b> while Attacking\" for one attack only.";
  public const string GoldenText = "Your first two minions that attack have \"<b>Immune</b> while Attacking\" for one attack only.";
  private readonly List<Minion> _targets = new List<Minion>();

  private int _maxTargets => this.DoubleIfGolden(1);

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return this._targets.Count >= this._maxTargets || this._targets.Contains(attacker) ? (Action) null : (Action) (() =>
    {
      this._targets.Add(attacker);
      attacker.ImmuneWhileAttackingCount = 1;
    });
  }
}
