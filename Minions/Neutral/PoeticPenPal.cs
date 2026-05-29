// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.PoeticPenPal
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class PoeticPenPal(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_105";
  public const string Text = "<b>Battlecry:</b> Reduce the Cost of your next <b>Buddy</b> by (2).";
  public const string GoldenText = "<b>Battlecry:</b> Reduce the Cost of your next <b>Buddy</b> by (4).";

  public Action? OnBattlecry() => (Action) null;
}
