// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.PrimevalMonstrosity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class PrimevalMonstrosity(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG33_320";
  public const string Text = "<b>Stealth</b>. Whenever a friendly <b>Rally</b> minion attacks, give a friendly minion of each type +{0}/+{1} permanently.";
  public const string GoldenText = "<b>Stealth</b>. Whenever a friendly <b>Rally</b> minion attacks, give a friendly minion of each type +{0}/+{1} permanently.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion _)
  {
    return (Action) (() =>
    {
      if (!attacker.HasRally())
        return;
      int attackBuff = this.DoubleIfGolden(3);
      int healthBuff = this.DoubleIfGolden(2);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().GetRandomPerRace())
        minion.IncreaseStats(attackBuff, healthBuff, (Entity) this);
    });
  }
}
