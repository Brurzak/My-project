// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.BabyYshaarj
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class BabyYshaarj(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_92_Buddy";
  public const string Text = "After you summon a minion of your Tier, give your minions +1/+1.";
  public const string GoldenText = "After you summon a minion of your Tier, give your minions +2/+2.";

  public Action OnAfterFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      if (summoned.tier != (this.ControlledByPlayer ? this.Simulator.PlayerState.Tier : this.Simulator.OpponentState.Tier))
        return;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive())))
        minion.IncreaseStats(this.DoubleIfGolden(1));
    });
  }
}
