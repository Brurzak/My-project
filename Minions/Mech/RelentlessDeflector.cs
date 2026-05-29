// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.RelentlessDeflector
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class RelentlessDeflector(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity,
  IOnFriendlyMinionGainDiv,
  IOnFriendlyMinionLostDiv
{
  public const string CardId = "BG34_405";
  public const string Text = "Has <b>Taunt</b> while this has <b>Divine Shield</b>. <b>Avenge ({0}):</b> Gain <b>Divine Shield</b>.";
  public const string GoldenText = "Has <b>Taunt</b> while this has <b>Divine Shield</b>. <b>Avenge ({0}):</b> Gain <b>Divine Shield</b>.";

  public int AvengeRequirement => 3;

  public Action? OnAvenge() => (Action) (() => this.div = 1);

  public Action? OnFriendlyMinionGainDiv(Minion gain)
  {
    return (Action) (() =>
    {
      if (gain != this)
        return;
      this.taunt = true;
    });
  }

  public Action? OnFriendlyMinionLostDiv(Minion lost)
  {
    return (Action) (() =>
    {
      if (lost != this)
        return;
      this.taunt = false;
    });
  }
}
