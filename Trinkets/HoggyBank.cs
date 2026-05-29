// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.HoggyBank
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class HoggyBank(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_MagicItem_411";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsQuilboar())))
        minion.AdditionalDeathrattles.Add(HoggyBank.Deathrattle());
    });
  }

  public static Action<Minion> Deathrattle()
  {
    return (Action<Minion>) (minion => minion.AddBloodGemToFriendlyHand());
  }
}
