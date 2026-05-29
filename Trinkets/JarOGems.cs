// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.JarOGems
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class JarOGems(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionAfterAttack,
  IEntity
{
  public const string CardId = "BG30_MagicItem_546";
  private int _triggers;

  public Action? OnFrienlyMinionAfterAttack(Minion attacker, Minion attackTarget)
  {
    return (Action) (() =>
    {
      if (++this._triggers % 2 == 1)
        return;
      foreach (Minion target in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsQuilboar() && x.IsAlive())))
        this.Simulator.CastBloodGem(target, (Entity) this);
    });
  }
}
