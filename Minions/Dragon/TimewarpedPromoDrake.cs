// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TimewarpedPromoDrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TimewarpedPromoDrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_088";
  public const string Text = "<b>Start of Combat:</b> Give your minions +{3}/+{3}. At the end of your turn, improve this.";
  public const string GoldenText = "<b>Start of Combat:</b> Give your minions +{3}/+{3}. At the end of your turn, improve this.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int scriptDataNum4 = this.ScriptDataNum4;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion.IncreaseStats(scriptDataNum4);
    });
  }
}
