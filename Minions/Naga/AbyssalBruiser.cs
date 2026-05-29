// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.AbyssalBruiser
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class AbyssalBruiser(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG35_921";
  public const string Text = "<b>Divine Shield</b> Has +{0}/+{1} for each Tavern spell you've cast this game.";
  public const string GoldenText = "<b>Divine Shield</b> Has +{0}/+{1} for each Tavern spell you've cast this game.";

  private int Counter
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.TavernSpellCounter : this.Simulator.state.Player.TavernSpellCounter;
    }
  }

  public override int SelfAttackPassive() => this.DoubleIfGolden(this.Counter);

  public override int SelfHealthPassive() => this.DoubleIfGolden(this.Counter);
}
