// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.BalefulIncense
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class BalefulIncense(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG32_MagicItem_360";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion first;
      Minion last;
      if (!this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsUndead() && !x.reborn)).ToList<Minion>().TryGetFirstAndLast<Minion>(out first, out last))
        return;
      first.reborn = true;
      last.reborn = true;
    });
  }
}
