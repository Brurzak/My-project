// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.SlammaSticker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class SlammaSticker(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG30_MagicItem_540";

  public Action? OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (!summoned.IsBeast() || summoned.IsDead())
        return;
      int num = 0;
      GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier != null)
        num = globalModifier.PassiveAttackBonusFor(summoned);
      if (summoned.CardID == "BG28_603t")
        num = (summoned.ControlledByPlayer ? this.Simulator.state.Player : this.Simulator.state.Opponent).BeetlesAtkBuff;
      summoned.IncreaseStats(summoned.attack() + num, 0);
    });
  }
}
