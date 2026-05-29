// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.RingBearer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells.TavernSpells;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class RingBearer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG34_921";
  public const string Text = "Whenever {1} friendly |4 (minion, minions) |4 (attacks, attack), cast Shiny Ring. <i>({0} left!)</i>";
  public const string GoldenText = "Whenever {1} friendly |4 (minion, minions) |4 (attacks, attack), cast Shiny Ring twice. <i>({0} left!)</i>";
  private static ITavernSpell _shinyRingSpell = (ITavernSpell) new ShinyRingSpell();
  private int _friendlyAttacksCounter;
  private int _remainingTriggers = 2;

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      ++this._friendlyAttacksCounter;
      if (this._remainingTriggers <= 0 || this._friendlyAttacksCounter % 2 == 1)
        return;
      this.Simulator.CastTavernSpell(RingBearer._shinyRingSpell, (Entity) this);
      if (this.golden)
        this.Simulator.CastTavernSpell(RingBearer._shinyRingSpell, (Entity) this);
      --this._remainingTriggers;
    });
  }
}
