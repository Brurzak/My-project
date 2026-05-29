// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Duos.SelflessSightseer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Duos;

public class SelflessSightseer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGDUO31_205";
  public const string Text = "<b>Battlecry:</b> Increase your team's maximum Gold by (1).";
  public const string GoldenText = "<b>Battlecry:</b> Increase your team's maximum Gold by (2).";

  public Action? OnBattlecry() => (Action) null;
}
