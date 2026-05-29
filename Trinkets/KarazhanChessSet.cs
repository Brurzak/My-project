// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.KarazhanChessSet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class KarazhanChessSet(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_972";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() => this.Simulator.TrySummonMinion((Summon) this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive())).ToList<Minion>()[0].Clone(), this.FriendlySide, 0, (Entity) this));
  }
}
