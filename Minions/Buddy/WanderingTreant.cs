// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.WanderingTreant
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class WanderingTreant(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacked,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_95_Buddy";
  public const string Text = "Whenever a friendly <b>Taunt</b> minion is attacked, give your minions +1/+1 permanently.";
  public const string GoldenText = "Whenever a friendly <b>Taunt</b> minion is attacked, give your minions +2/+2 permanently.";

  public Action OnFriendlyMinionIsAttacked(Minion friendlyMinion, Minion attacker)
  {
    return (Action) (() =>
    {
      if (!friendlyMinion.taunt)
        return;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion.IncreaseStats(this.DoubleIfGolden(1));
    });
  }
}
