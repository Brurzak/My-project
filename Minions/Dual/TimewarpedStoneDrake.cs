// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.TimewarpedStoneDrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class TimewarpedStoneDrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_675";
  public const string Text = "<b>Start of Combat:</b> Gain the stats of all the minions you sold this turn. <i>({0}/{1})</i>";
  public const string GoldenText = "<b>Start of Combat:</b> Gain double the stats of all the minions you sold this turn. <i>({0}/{1})</i>";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() => this.IncreaseStats(this.ScriptDataNum1, this.ScriptDataNum2));
  }
}
