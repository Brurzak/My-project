// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LuckyTabby
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using HearthDb.Enums;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LuckyTabby(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG30_MagicItem_931";
  private int _triggers;

  public Action? OnAfterFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!died.IsBeast() || ++this._triggers % 6 != 0)
        return;
      Card card;
      if (!this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 20).TryGetRandom<Card>(out card))
        this.AddMinionToFriendlyHand();
      else
        this.AddMinionToFriendlyHand(card.Id);
    });
  }
}
