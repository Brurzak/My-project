// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.VoidPupTrainer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class VoidPupTrainer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG35_152";
  public const string Text = "<b>Battlecry:</b> Give minions in the Tavern from Tier 3 and below +{0}/+{1} this game.";
  public const string GoldenText = "<b>Battlecry:</b> Give minions in the Tavern from Tier 3 and below +{0}/+{1} this game.";

  public Action? OnBattlecry() => (Action) null;
}
