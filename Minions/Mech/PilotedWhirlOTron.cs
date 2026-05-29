// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.PilotedWhirlOTron
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Minions.Undead;
using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class PilotedWhirlOTron(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG21_HERO_030_Buddy";
  public const string Text = "<b>Start of Combat:</b> Copy your two left-most <b>Deathrattles</b> <i>(except other Whirl-O-Trons)</i>.";
  public const string GoldenText = "<b>Start of Combat:</b> Copy your two left-most <b>Deathrattles</b> twice <i>(except other Whirl-O-Trons)</i>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.CardID != this.CardID && x.IsAlive() && x.HasDeathrattle())).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      foreach (Minion minion in list)
      {
        for (int index = 0; index < this.DoubleIfGolden(1); ++index)
        {
          foreach (IDeathrattle trigger in minion.GetTriggers<IDeathrattle>())
          {
            Action<Minion> deathrattle = trigger.GetDeathrattle();
            if (deathrattle != null && (!(minion is StitchedSalvager stitchedSalvager2) || stitchedSalvager2.DeathrattleInitialized()))
              this.AdditionalDeathrattles.Add(deathrattle);
          }
          if (minion.AdditionalDeathrattles.Any<Action<Minion>>())
            this.AdditionalDeathrattles.AddRange((IEnumerable<Action<Minion>>) minion.AdditionalDeathrattles);
        }
      }
    });
  }
}
