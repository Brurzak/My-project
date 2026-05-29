// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.RotHideGnoll
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class RotHideGnoll(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG25_013";
  public const string Text = "Has +1 Attack for each friendly minion that died this combat.";
  public const string GoldenText = "Has +2 Attack for each friendly minion that died this combat.";

  public override int SelfAttackPassive()
  {
    return this.DoubleIfGolden((this.ControlledByPlayer ? this.Simulator.state.Player.DeadMinions : this.Simulator.state.Opponent.DeadMinions).Count);
  }
}
