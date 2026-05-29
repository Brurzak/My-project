// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.SlimyFelblood
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class SlimyFelblood(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG29_873";
  public const string Text = "<b>Battlecry:</b> Minions in the Tavern have +2 Health this game.";
  public const string GoldenText = "<b>Battlecry:</b> Minions in the Tavern have +4 Health this game.";

  public Action? OnBattlecry() => (Action) null;
}
