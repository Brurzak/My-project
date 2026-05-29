// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.AzsharanCutlassier
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class AzsharanCutlassier(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG33_830";
  public const string Text = "<b>Battlecry:</b> Your Tavern spells give an extra +1 Attack this game.";
  public const string GoldenText = "<b>Battlecry:</b> Your Tavern spells give an extra +2 Attack this game.";

  public Action? OnBattlecry() => (Action) null;
}
