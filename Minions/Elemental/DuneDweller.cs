// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.DuneDweller
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class DuneDweller(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_815";
  public const string Text = "<b>Battlecry:</b> Give Elementals in the Tavern +{0}/+{1} this game.";
  public const string GoldenText = "<b>Battlecry:</b> Give Elementals in the Tavern +{0}/+{1} this game twice.";

  public Action? OnBattlecry() => (Action) null;
}
