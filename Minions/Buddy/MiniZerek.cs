// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.MiniZerek
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class MiniZerek(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_HERO_005_Buddy";
  public const string Text = "<b>Battlecry:</b> Choose a minion in the Tavern. Transform into a copy of it.";
  public const string GoldenText = "<b>Battlecry:</b> Choose a minion in the Tavern. Transform into a Golden copy of it.";

  public Action? OnBattlecry() => (Action) null;
}
