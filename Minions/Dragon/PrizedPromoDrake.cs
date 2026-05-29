// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.PrizedPromoDrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class PrizedPromoDrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG21_014";
  public const string Text = "<b>Start of Combat:</b> Give your Dragons +{0}/+{1}.";
  public const string GoldenText = "<b>Start of Combat:</b> Give your Dragons +{0}/+{1}.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsDragon())))
        minion.IncreaseStats(this.DoubleIfGolden(4), this.DoubleIfGolden(4));
    });
  }
}
