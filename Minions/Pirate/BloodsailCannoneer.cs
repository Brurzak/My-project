// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.BloodsailCannoneer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class BloodsailCannoneer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGS_053";
  public const string Text = "<b>Battlecry</b>: Give your other Pirates +3 Attack.";
  public const string GoldenText = "<b>Battlecry</b>: Give your other Pirates +6 Attack.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(3);
      foreach (Minion minion in this.FriendlySide)
      {
        if (minion != this && minion.IsPirate() && !minion.IsDead())
          minion.IncreaseStats(attackBuff, 0, (Entity) this);
      }
    });
  }
}
