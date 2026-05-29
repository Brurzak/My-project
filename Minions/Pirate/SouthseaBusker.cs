// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.SouthseaBusker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class SouthseaBusker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG26_135";
  public const string Text = "<b>Battlecry:</b> Gain 1 Gold next turn.";
  public const string GoldenText = "<b>Battlecry:</b> Gain 2 Gold next turn.";

  public Action? OnBattlecry() => (Action) null;
}
