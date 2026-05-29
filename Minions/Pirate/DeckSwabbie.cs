// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.DeckSwabbie
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class DeckSwabbie(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGS_055";
  public const string Text = "<b>Battlecry:</b> Reduce the Cost of upgrading the Tavern by (1).";
  public const string GoldenText = "<b>Battlecry:</b> Reduce the Cost of upgrading the Tavern by (2).";

  public Action? OnBattlecry() => (Action) null;
}
