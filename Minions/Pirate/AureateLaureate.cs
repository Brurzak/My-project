// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.AureateLaureate
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class AureateLaureate(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG32_236";
  public const string Text = "<b>Divine Shield</b> <b>Battlecry:</b> Make this minion Golden.";
  public const string GoldenText = "<b>Divine Shield</b> <b>Battlecry:</b> Make this minion Golden.";

  public Action? OnBattlecry() => (Action) (() => this.TryMakeGolden(false, (Entity) this));
}
