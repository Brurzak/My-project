// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.BlueVolumizer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class BlueVolumizer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnMagnetize,
  IEntity
{
  public const string CardId = "BG34_170t2";
  public const string Text = "<b>Magnetic</b>. The first time this is played or <b>Magnetized</b>, your Volumizers have +{1} Health this game <i>(wherever they are)</i>.";
  public const string GoldenText = "<b>Magnetic</b>. The first time this is played or <b>Magnetized</b>, your Volumizers have +{1} Health this game <i>(wherever they are)</i>.";
  public const int HealthBuff = 3;

  public override int SelfAttackPassive()
  {
    return !this.ControlledByPlayer ? this.Simulator.state.Opponent.VolumizerAtkBuff : this.Simulator.state.Player.VolumizerAtkBuff;
  }

  public override int SelfHealthPassive()
  {
    return !this.ControlledByPlayer ? this.Simulator.state.Opponent.VolumizerHealthBuff : this.Simulator.state.Player.VolumizerHealthBuff;
  }

  public Action? OnMagnetize()
  {
    return (Action) (() => (this.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent).VolumizerHealthBuff += this.DoubleIfGolden(3));
  }
}
