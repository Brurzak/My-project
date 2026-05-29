// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedHawkstrider
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedHawkstrider(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_370";
  public const string Text = "<b>Start of Combat:</b> Trigger all friendly <b>Deathrattles</b>.";
  public const string GoldenText = "<b>Start of Combat:</b> Trigger all friendly <b>Deathrattles</b> twice.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.HasDeathrattle() && x.IsAlive())).ToList<Minion>())
        {
          this.Simulator.TriggerDeathrattles(minion);
          this.Simulator.ResolveTriggersInCurrentScope();
        }
      }
    });
  }
}
