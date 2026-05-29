// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserBelcherPortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserBelcherPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionLostVenomous,
  IEntity
{
  public const string CardId = "BG30_MagicItem_432";

  public Action? OnFriendlyMinionLostVenomous(Minion minion)
  {
    return (Action) (() => minion.IncreaseStats(4));
  }
}
