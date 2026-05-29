// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.FallingSkyGolem
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class FallingSkyGolem(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG35_342";
  public const string Text = "<b>Divine Shield</b>. Has +{0}/+{1} for each <b>Deathrattle</b> you've triggered this game <i>(wherever this is).</i>";
  public const string GoldenText = "<b>Divine Shield</b>. Has +{0}/+{1} for each <b>Deathrattle</b> you've triggered this game <i>(wherever this is).</i>";

  public override int SelfAttackPassive() => this.DoubleIfGolden(this.Counter * 4);

  public override int SelfHealthPassive() => this.DoubleIfGolden(this.Counter * 2);

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.DeathrattleCounter : this.Simulator.state.Player.DeathrattleCounter;
    }
  }
}
