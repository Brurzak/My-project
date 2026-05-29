// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.StormcoilSticker
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

public class StormcoilSticker(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG35_MagicItem_302";
  private const int AvengeRequirement = 7;
  private int _avengeCounter;
  private bool _isFirstDeath = true;

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (this._isFirstDeath)
      {
        this._isFirstDeath = false;
        this._avengeCounter = this.ScriptDataNum1;
      }
      ++this._avengeCounter;
      if (this._avengeCounter % 7 != 0)
        return;
      Card card;
      if (!this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 17).TryGetRandom<Card>(out card))
        this.AddMinionToFriendlyHand();
      else
        this.AddMinionToFriendlyHand(card.Id);
      this._avengeCounter = 0;
    });
  }
}
