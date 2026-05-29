// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.RecklessCliffdiver
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class RecklessCliffdiver(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyBattlecry,
  IEntity
{
  public const string CardId = "BG31_142";
  public const string Text = "<b>Reborn</b>. Has +1 Attack for each <b>Battlecry</b> you've triggered this game <i>(wherever this is)</i>.";
  public const string GoldenText = "<b>Reborn</b>. Has +2 Attack for each <b>Battlecry</b> you've triggered this game <i>(wherever this is)</i>.";
  private int _inCombatCounter;

  public override int SelfAttackPassive() => this.DoubleIfGolden(this.Counter);

  public override int SelfHealthPassive() => this.DoubleIfGolden(this.Counter);

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.BattlecryCounter + this._inCombatCounter : this.Simulator.state.Player.BattlecryCounter + this._inCombatCounter;
    }
  }

  public Action? OnFriendlyBattlecry() => (Action) (() => ++this._inCombatCounter);
}
