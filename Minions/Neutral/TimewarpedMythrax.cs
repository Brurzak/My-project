// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TimewarpedMythrax
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class TimewarpedMythrax(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_684";
  public const string Text = "<b>Start of Combat:</b> Gain the stats of 3 friendly minions of different types <i>(except Timewarped Mythrax)</i>.";
  public const string GoldenText = "<b>Start of Combat:</b> Gain twice the stats of 3 friendly minions of different types. <i>(except Timewarped Mythrax)</i>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      foreach (Minion randomElement in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.CardID != "BG34_Giant_684")).ToList<Minion>().GetRandomPerRace().GetRandomElements<Minion>(3))
        this.IncreaseStats(randomElement.attack() * num, randomElement.health() * num);
    });
  }
}
