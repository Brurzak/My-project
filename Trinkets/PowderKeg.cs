// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.PowderKeg
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Minions.Pirate;
using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class PowderKeg(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG35_MagicItem_714";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion randomElement in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsPirate())).ToList<Minion>().GetRandomElements<Minion>(2))
        randomElement.AdditionalDeathrattles.Add(ShipJumper.Deathrattle(false));
    });
  }
}
