// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.BoundingFelstalker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class BoundingFelstalker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG32_893";
  public const string Text = "<b>Battlecry:</b> <b>Refresh</b> the Tavern with 2 extra Tavern spells. They cost Health instead of Gold.";
  public const string GoldenText = "<b>Battlecry:</b> <b>Refresh</b> the Tavern with 2 extra Tavern spells. They cost Health instead of Gold.";

  public Action? OnBattlecry() => (Action) null;
}
