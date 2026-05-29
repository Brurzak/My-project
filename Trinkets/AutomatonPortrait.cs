// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.AutomatonPortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class AutomatonPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity,
  IOnAfterAttackStep
{
  public const string CardId = "BG30_MagicItem_303";
  private bool _summoned;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Count == 7)
        return;
      this.Simulator.TrySummonMinion((Summon) "BG_TTN_401", this.FriendlySide, this.FriendlySide.Count, (Entity) this);
      this._summoned = true;
    });
  }

  public void OnAfterAttackStep()
  {
    if (this._summoned || this.FriendlySide.Count == 7)
      return;
    this.Simulator.TrySummonMinion((Summon) "BG_TTN_401", this.FriendlySide, this.FriendlySide.Count, (Entity) this);
    this._summoned = true;
  }
}
