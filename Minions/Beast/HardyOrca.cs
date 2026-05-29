// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.HardyOrca
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class HardyOrca(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG34_312";
  public const string Text = "<b>Taunt</b> Whenever this takes damage, give your other minions +{0}/+{1}.";
  public const string GoldenText = "<b>Taunt</b> Whenever this takes damage, give your other minions +{0}/+{1}.";

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive())))
        minion.IncreaseStats(this.DoubleIfGolden(1));
    });
  }
}
