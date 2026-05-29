// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.WrathscaleRogue
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class WrathscaleRogue(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG33_920";
  public const string Text = "After another friendly Naga gains Health, give it that much Attack.";
  public const string GoldenText = "After another friendly Naga gains Health, give it twice that much Attack.";

  public Action? OnFriendlyMinionBuffed(
    Minion minion,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return minion == this || !minion.IsNaga() || healthChange <= 0 ? (Action) null : (Action) (() => minion.IncreaseStats(this.golden ? healthChange * 2 : healthChange, 0));
  }
}
