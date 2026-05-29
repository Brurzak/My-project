// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.LordOfTheRuins
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class LordOfTheRuins(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionDealsDamage,
  IEntity
{
  public const string CardId = "BG33_154";
  public const string Text = "After a friendly Demon deals damage, give friendly minions other than it +{0}/+{1}.";
  public const string GoldenText = "After a friendly Demon deals damage, give friendly minions other than it +{0}/+{1}.";

  public Action? OnFriendlyMinionDealsDamage(Minion source, Minion _, int value)
  {
    return (Action) (() =>
    {
      if (!source.IsDemon())
        return;
      foreach (Minion minion in source.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != source && x.IsAlive())))
        minion.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(1));
    });
  }
}
