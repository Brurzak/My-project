// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.BristlebackKnight
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class BristlebackKnight(string cardId, bool controlledByPlayer, Simulator simulator) : Minion(cardId, controlledByPlayer, simulator)
{
  public const string CardId = "BG20_204";
  public const string Text = "<b>Windfury</b>, <b>Divine Shield</b> The first time this survives damage each combat, gain <b><b>Divine Shield</b>.</b>";
  public const string GoldenText = "<b>Windfury</b>, <b>Divine Shield</b> The first 2 times this survives damage each combat, gain <b><b>Divine Shield</b>.</b>";

  public override Action? OnFirstTimeTakenDamage()
  {
    return (Action) (() =>
    {
      if (!this.IsAlive())
        return;
      this.div = 1;
    });
  }

  public override Action? OnSecondTimeTakenDamage()
  {
    return (Action) (() =>
    {
      if (!this.golden || !this.IsAlive())
        return;
      this.div = 1;
    });
  }
}
