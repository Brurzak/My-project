// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.FleetingVigor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Spells;

public class FleetingVigor(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG28_519";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide)
        minion.IncreaseStats(this.ScriptDataNum1, this.ScriptDataNum2);
    });
  }
}
