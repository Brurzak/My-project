// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.BroodOfNozdormu
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Spells;

public class BroodOfNozdormu(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_889";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion minion = this.FriendlySide.FirstOrDefault<Minion>((Func<Minion, bool>) (m => m.IsAlive()));
      minion?.IncreaseStats(minion.attack(), 0);
    });
  }
}
