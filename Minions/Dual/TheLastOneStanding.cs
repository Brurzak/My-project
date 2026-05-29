// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.TheLastOneStanding
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class TheLastOneStanding(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_320";
  public const string Text = "<b>Rally:</b> Give a friendly minion of each type +{0}/+{1} permanently.";
  public const string GoldenText = "<b>Rally:</b> Give a friendly minion of each type +{0}/+{1} permanently, twice.";

  private int AttackBuff => this.DoubleIfGolden(12);

  private int HealthBuff => this.DoubleIfGolden(12);

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is TheLastOneStanding theLastOneStanding2))
        return;
      foreach (Minion minion1 in minion.FriendlySide.GetRandomPerRace())
        minion1.IncreaseStats(theLastOneStanding2.AttackBuff, theLastOneStanding2.HealthBuff, (Entity) this);
    });
  }
}
