// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.GreaterTigerCarving
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class GreaterTigerCarving(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionTakeDamage,
  IEntity
{
  public const string CardId = "BG30_MagicItem_427t";

  public Action? OnFriendlyMinionTakeDamage(Minion target, int value)
  {
    return (Action) (() =>
    {
      Minion minion;
      if (!this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion))
        return;
      minion.IncreaseStats(4, 0);
    });
  }
}
