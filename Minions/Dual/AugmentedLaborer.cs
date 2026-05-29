// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.AugmentedLaborer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class AugmentedLaborer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG28_740";
  public const string Text = "<b>Avenge (4):</b> Get a random <b>Magnetic</b> Mecha-Demon.";
  public const string GoldenText = "<b>Avenge (4):</b> Get 2 random <b>Magnetic</b> Mecha-Demons.";
  public static readonly List<string> MechaDemonCardIds = new List<string>()
  {
    "BG25_807t3",
    "BG25_807t2",
    "BG25_807t",
    "BG25_807t4"
  };

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        this.AddMinionToFriendlyHand(AugmentedLaborer.MechaDemonCardIds.GetRandom<string>());
    });
  }
}
