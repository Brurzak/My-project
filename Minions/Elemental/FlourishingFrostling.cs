// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.FlourishingFrostling
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class FlourishingFrostling(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG26_537";
  public const string Text = "Has +2/+1 for each Elemental you played this game <i>(wherever this is)</i>.";
  public const string GoldenText = "Has +4/+2 for each Elemental you played this game <i>(wherever this is)</i>.";

  public override int SelfAttackPassive() => this.DoubleIfGolden(2 * this.Counter);

  public override int SelfHealthPassive() => this.DoubleIfGolden(this.Counter);

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.ElementalPlayCounter : this.Simulator.state.Player.ElementalPlayCounter;
    }
  }
}
