// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.TimewarpedBusker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class TimewarpedBusker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG34_Giant_001";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Gain {0} Gold next turn.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Gain {0} Gold next turn.";

  public Action? OnBattlecry() => (Action) null;

  public Action<Minion>? GetDeathrattle() => (Action<Minion>) (minion => { });
}
