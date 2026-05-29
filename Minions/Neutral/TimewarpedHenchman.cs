// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TimewarpedHenchman
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class TimewarpedHenchman(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionKilledEnemy,
  IEntity
{
  public const string CardId = "BG34_Giant_593";
  public const string Text = "After you kill a second minion each combat, get a plain copy of it.";
  public const string GoldenText = "After you kill a second minion each combat, get 2 plain copies of it.";
  private bool triggered;
  private int pendingDeaths;

  public Action? OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return this.triggered ? (Action) null : (Action) (() =>
    {
      if (this.triggered)
        return;
      List<Minion> minionList = this.ControlledByPlayer ? this.Simulator.state.Opponent.DeadMinions : this.Simulator.state.Player.DeadMinions;
      ++this.pendingDeaths;
      if (minionList.Count + this.pendingDeaths < 2)
        return;
      this.triggered = true;
      this.AddMinionToFriendlyHand(killed.CardID);
      if (!this.golden)
        return;
      this.AddMinionToFriendlyHand(killed.CardID);
    });
  }
}
