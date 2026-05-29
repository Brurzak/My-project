// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.BlueChromadrakeToken
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class BlueChromadrakeToken(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_634t";
  public const string Text = "<b>Battlecry:</b> Get a random {0}-Cost Tavern spell.";
  public const string GoldenText = "<b>Battlecry:</b> Get two random {0}-Cost Tavern spells.";

  public Action? OnBattlecry() => (Action) null;
}
