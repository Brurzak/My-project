// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.OminousSeer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class OminousSeer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_330";
  public const string Text = "<b>Battlecry:</b> The next Tavern spell you buy costs (1) less.";
  public const string GoldenText = "<b>Battlecry:</b> The next Tavern spell you buy costs (2) less.";

  public Action? OnBattlecry() => (Action) null;
}
