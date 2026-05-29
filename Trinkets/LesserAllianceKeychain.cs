// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserAllianceKeychain
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserAllianceKeychain(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionDied,
  IEntity
{
  public const string CardId = "BG30_MagicItem_433";
  private bool _isFirstFriendlyToDie = true;

  public Action? OnFriendlyMinionDied(Minion died, Minion? leftNeighbor, Minion? rightNeighbor)
  {
    return (Action) (() =>
    {
      if (!this._isFirstFriendlyToDie)
        return;
      Minion minion;
      if (died.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != died && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion))
        minion.IncreaseStats(died.maxAttack, died.maxHealth);
      this._isFirstFriendlyToDie = false;
    });
  }
}
