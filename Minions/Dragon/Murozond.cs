// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Murozond
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class Murozond(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGS_043";
  public const string Text = "<b>Battlecry:</b> Get a plain copy of a minion from your last opponent's warband.";
  public const string GoldenText = "<b>Battlecry:</b> Get a plain copy of a minion from your last opponent's warband, twice.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.ControlledByPlayer ? this.Simulator.opponentCloningGallery.Minions : this.Simulator.playerCloningGallery.Minions;
      Minion minion;
      for (int index = 0; index < this.DoubleIfGolden(1) && list.TryGetRandom<Minion>(out minion); ++index)
        this.AddCardToFriendlyHand((CardEntity) new MinionCardEntity(minion.Simulator.MinionFactory.CreateFromCardId(minion.CardID, minion.ControlledByPlayer), (Entity) this, this.Simulator));
    });
  }
}
