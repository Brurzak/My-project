// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Malorne
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Malorne(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG32_HERO_001_Buddy";
  public const string Text = "Has +1/+1 for every 3 Gold you've spent this game.";
  public const string GoldenText = "Has +2/+2 for every 3 Gold you've spent this game.";

  public override int SelfAttackPassive() => this.DoubleIfGolden(this.GlobalCounter);

  public override int SelfHealthPassive() => this.DoubleIfGolden(this.GlobalCounter);

  private int GlobalCounter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.ResourcesSpentThisGame / 3 : this.Simulator.state.Player.ResourcesSpentThisGame / 3;
    }
  }
}
