// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.Beetle
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class Beetle(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG28_603t";
  public const string Text = "";
  public const string GoldenText = "";

  public override int SelfAttackPassive()
  {
    return !this.ControlledByPlayer ? this.Simulator.state.Opponent.BeetlesAtkBuff : this.Simulator.state.Player.BeetlesAtkBuff;
  }

  public override int SelfHealthPassive()
  {
    return !this.ControlledByPlayer ? this.Simulator.state.Opponent.BeetlesHealthBuff : this.Simulator.state.Player.BeetlesHealthBuff;
  }
}
