// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.CeremonialSword
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class CeremonialSword(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG30_MagicItem_925";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() => attacker.IncreaseStats(4, 0));
  }
}
