// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.IniStormcoilHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class IniStormcoilHeroPower : HeroPower, IAvenge, IEntity
{
  public IniStormcoilHeroPower(
    string cardId,
    Simulator simulator,
    bool controlledByPlayer,
    HeroPowerData data)
    : base(cardId, simulator, controlledByPlayer, data)
  {
    this.AvengeCounter = this.Data.Data;
  }

  public int AvengeCounter { get; set; }

  public int AvengeRequirement => 9;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      Card card;
      if (!this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 17).TryGetRandom<Card>(out card))
        this.AddMinionToFriendlyHand();
      else
        this.AddMinionToFriendlyHand(card.Id);
    });
  }
}
