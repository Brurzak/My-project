// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.SpellboundSoul
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class SpellboundSoul(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG34_110";
  public const string Text = "Has +{0}/+{1} for each Tavern spell you've cast this game <i>(wherever this is)</i>.";
  public const string GoldenText = "Has +{0}/+{1} for each Tavern spell you've cast this game <i>(wherever this is)</i>.";

  public override int SelfAttackPassive() => this.DoubleIfGolden(this.Counter * 2);

  public override int SelfHealthPassive() => this.DoubleIfGolden(this.Counter);

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.TavernSpellCounter : this.Simulator.state.Player.TavernSpellCounter;
    }
  }
}
