// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.UnforgivingTreant
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class UnforgivingTreant(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG29_846";
  public const string Text = "<b>Taunt</b> Whenever this takes damage, give your minions +{0} Attack permanently.";
  public const string GoldenText = "<b>Taunt</b> Whenever this takes damage, give your minions +{0} Attack permanently.";

  public Action OnTakeDamage(int value)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion.IncreaseStats(this.DoubleIfGolden(2), 0);
    });
  }
}
