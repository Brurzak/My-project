// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.GolemArchivist
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class GolemArchivist(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG33_870";
  public const string Text = "<b>Battlecry:</b> The next Tavern spell you buy costs (2) less.";
  public const string GoldenText = "<b>Battlecry:</b> The next two Tavern spells you buy cost (2) less.";

  public Action? OnBattlecry() => (Action) null;
}
