// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.ReverberDrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class ReverberDrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_300";
  public const string Text = "<b>Battlecry:</b> Cast <b>Echo</b>ing Roar on a friendly Dragon.";
  public const string GoldenText = "<b>Battlecry:</b> Cast <b>Echo</b>ing Roar on a friendly Dragon twice.";

  public Action? OnBattlecry() => (Action) null;
}
