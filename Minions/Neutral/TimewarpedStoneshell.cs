// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TimewarpedStoneshell
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class TimewarpedStoneshell(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_601";
  public const string Text = "<b>Start of Combat:</b> Copy all friendly <b>Rallies</b> <i>(except other Stoneshells)</i>.";
  public const string GoldenText = "<b>Start of Combat:</b> Copy all friendly <b>Rallies</b> twice <i>(except other Stoneshells)</i>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion1 in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.HasRally() && x.IsAlive() && x.CardID != "BG34_Giant_601" && x.CardID != "BG33_HERO_000_Buddy")))
      {
        Minion r = minion1;
        for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        {
          foreach (IOnRally trigger in r.GetTriggers<IOnRally>())
          {
            IOnRally idr = trigger;
            this.AdditionalRallies.Add((Action<Minion>) (minion => idr.OnRally(r.golden, r)(minion)));
          }
          if (r.AdditionalRallies.Any<Action<Minion>>())
            this.AdditionalRallies.AddRange((IEnumerable<Action<Minion>>) r.AdditionalRallies);
        }
      }
    });
  }
}
