// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.MechaJaraxxus
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class MechaJaraxxus(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_807";
  public const string Text = "<b>Battlecry:</b> Get a random <b>Magnetic</b> Mecha-Demon.";
  public const string GoldenText = "<b>Battlecry:</b> Get 2 random <b>Magnetic</b> Mecha-Demons.";
  public static readonly List<string> MechaDemonCardIds = new List<string>()
  {
    "BG25_807t3",
    "BG25_807t2",
    "BG25_807t",
    "BG25_807t4"
  };

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        this.AddMinionToFriendlyHand(MechaJaraxxus.MechaDemonCardIds.GetRandom<string>());
    });
  }
}
