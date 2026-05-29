// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.IronforgeAnvil
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class IronforgeAnvil(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_403";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion target in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsNoType())))
      {
        int num1 = 0;
        int num2 = 0;
        GlobalModifier globalModifier = this.ControlledByPlayer ? this.Simulator.state.Player.GlobalModifier : this.Simulator.state.Opponent.GlobalModifier;
        if (globalModifier != null)
        {
          num1 = globalModifier.PassiveAttackBonusFor(target);
          num2 = globalModifier.PassiveHealthBonusFor(target);
        }
        target.IncreaseStats(target.attack() * 2 + num1, target.health() * 2 + num2);
      }
    });
  }
}
