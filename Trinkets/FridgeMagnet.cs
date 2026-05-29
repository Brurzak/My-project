// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.FridgeMagnet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class FridgeMagnet(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IAvenge,
  IEntity
{
  public const string CardId = "BG30_MagicItem_545";

  public int AvengeCounter { get; set; }

  public int AvengeRequirement { get; } = 3;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      Card card;
      if (!this.Simulator.MinionFactory.MinionPoolOptionsPlayer(this.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Entity.GetTag((GameTag) 849) > 0)).ToList<Card>().TryGetRandom<Card>(out card))
        this.AddMinionToFriendlyHand();
      else
        this.AddMinionToFriendlyHand(card.Id);
    });
  }
}
