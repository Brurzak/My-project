// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.ConveyorConstruct
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class ConveyorConstruct(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_171";
  public const string Text = "<b>Deathrattle:</b> Get a random <b>Magnetic</b> Volumizer.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 random <b>Magnetic</b> Volumizers.";
  public static readonly List<string> VolumizerCardIds = new List<string>()
  {
    "BG34_170t3",
    "BG34_170t",
    "BG34_170t2"
  };

  public Action<Minion> GetDeathrattle() => ConveyorConstruct.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddMinionToFriendlyHand(ConveyorConstruct.VolumizerCardIds.GetRandom<string>());
      if (!golden)
        return;
      minion.AddMinionToFriendlyHand(ConveyorConstruct.VolumizerCardIds.GetRandom<string>());
    });
  }
}
