// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.InterrogatorWhitemane
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class InterrogatorWhitemane(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG24_704";
  public const string Text = "<b>Start of Combat:</b> Give an enemy minion from Tier 5 or higher <b>Taunt</b>. It takes double damage this combat.";
  public const string GoldenText = "<b>Start of Combat:</b> Give two enemy minions from Tier 5 or higher <b>Taunt</b>. They take double damage this combat.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      List<Minion> list = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (m => m.tier >= 5)).ToList<Minion>();
      Minion minion;
      for (int index = 0; index < num && list.TryGetRandom<Minion>(out minion); ++index)
      {
        minion.taunt = true;
        minion.DamageMultiplier = 2;
        list.Remove(minion);
      }
    });
  }
}
