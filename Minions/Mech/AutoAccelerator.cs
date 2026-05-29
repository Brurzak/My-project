// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.AutoAccelerator
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class AutoAccelerator(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_170";
  public const string Text = "<b>Battlecry:</b> Get a random <b>Magnetic</b> Volumizer.";
  public const string GoldenText = "<b>Battlecry:</b> Get 2 random <b>Magnetic</b> Volumizers.";
  public static readonly List<string> VolumizerCardIds = new List<string>()
  {
    "BG34_170t3",
    "BG34_170t",
    "BG34_170t2"
  };

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.AddMinionToFriendlyHand(AutoAccelerator.VolumizerCardIds.GetRandom<string>());
      if (!this.golden)
        return;
      this.AddMinionToFriendlyHand(AutoAccelerator.VolumizerCardIds.GetRandom<string>());
    });
  }
}
