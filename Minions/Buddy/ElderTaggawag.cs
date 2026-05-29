// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.ElderTaggawag
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class ElderTaggawag(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_14_Buddy";
  public const string Text = "<b>Start of Combat:</b> If you control 4 minions with different types, gain your minions' highest Attack and Health.";
  public const string GoldenText = "<b>Start of Combat:</b> If you control 4 minions with different types, gain double your minions' highest Attack and Health.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.GetRandomPerRace().Count < 4)
        return;
      int i1 = this.FriendlySide.Max<Minion>((Func<Minion, int>) (x => x.attack()));
      int i2 = this.FriendlySide.Max<Minion>((Func<Minion, int>) (x => x.health()));
      this.IncreaseStats(this.DoubleIfGolden(i1), this.DoubleIfGolden(i2));
    });
  }
}
