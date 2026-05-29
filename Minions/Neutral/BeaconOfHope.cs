// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.BeaconOfHope
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class BeaconOfHope(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG32_869";
  public const string Text = "<b>Battlecry:</b> If you lost your last combat, <b>Discover</b> a Tier 5 minion.";
  public const string GoldenText = "<b>Battlecry:</b> If you lost your last combat, <b>Discover</b> two Tier 5 minions.";

  public Action? OnBattlecry() => (Action) null;
}
