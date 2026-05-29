// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TwilightWatcher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TwilightWatcher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG33_245";
  public const string Text = "Whenever a friendly Dragon attacks, give your Dragons +{0}/+{1}.";
  public const string GoldenText = "Whenever a friendly Dragon attacks, give your Dragons +{0}/+{1}.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      if (!attacker.IsDragon())
        return;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsDragon())).ToList<Minion>())
        minion.IncreaseStats(this.DoubleIfGolden(1), this.DoubleIfGolden(2));
    });
  }
}
