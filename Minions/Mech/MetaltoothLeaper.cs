// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.MetaltoothLeaper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class MetaltoothLeaper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG_GVG_048";
  public const string Text = "<b>Battlecry:</b> Give your other Mechs +2 Attack.";
  public const string GoldenText = "<b>Battlecry:</b> Give your other Mechs +4 Attack.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(2);
      foreach (Minion minion in this.FriendlySide)
      {
        if (minion != this && minion.IsMech())
          minion.IncreaseStats(attackBuff, 0);
      }
    });
  }
}
