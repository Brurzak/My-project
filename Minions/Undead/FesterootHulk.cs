// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.FesterootHulk
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class FesterootHulk(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionAfterAttack,
  IEntity
{
  public const string CardId = "BG_GIL_655";
  public const string Text = "After a friendly minion attacks, gain +1 Attack.";

  public Action? OnFrienlyMinionAfterAttack(Minion attacker, Minion target)
  {
    return (Action) (() => this.AttachedOrThis.IncreaseStats(this.DoubleIfGolden(1), 0));
  }
}
