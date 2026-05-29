// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.BlazingSkyfin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class BlazingSkyfin(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyBattlecry,
  IEntity
{
  public const string CardId = "BG25_040";
  public const string Text = "After you trigger a <b>Battlecry</b>, gain +1/+1.";
  public const string GoldenText = "After you trigger a <b>Battlecry</b>, gain +2/+2.";

  public Action? OnFriendlyBattlecry()
  {
    return (Action) (() => this.IncreaseStats(this.DoubleIfGolden(1), (Entity) this));
  }
}
