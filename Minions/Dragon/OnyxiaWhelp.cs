// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.OnyxiaWhelp
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class OnyxiaWhelp(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnSummoned,
  IEntity
{
  public const string CardId = "BG22_HERO_305t";
  public const string Text = "";
  public const string GoldenText = "";

  public Action? OnSummoned() => (Action) (() => this.Simulator.AttackWithMinion((Minion) this));
}
