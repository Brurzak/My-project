// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.RokaraHeroPower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.HeroPowers;

public class RokaraHeroPower(
  string cardId,
  Simulator simulator,
  bool controlledByPlayer,
  HeroPowerData data) : HeroPower(cardId, simulator, controlledByPlayer, data), IOnFriendlyMinionKilledEnemy, IEntity
{
  public Action OnFriendlyMinionKilledEnemy(Minion friendly, Minion killed)
  {
    return (Action) (() => friendly.IncreaseStats(1, 0));
  }
}
