// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.VanessaVancleef
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class VanessaVancleef(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAttack,
  IEntity
{
  public const string CardId = "BG24_708";
  public const string Text = "<b>Rally:</b> Give your Pirates +2/+1 permanently.";
  public const string GoldenText = "<b>Rally:</b> Give your Pirates +4/+2 permanently.";

  public Action? OnAttack(Minion target)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide)
      {
        if (minion.IsPirate())
          minion.IncreaseStats(this.DoubleIfGolden(2), this.DoubleIfGolden(1), (Entity) this);
      }
    });
  }
}
