// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.ProdigiousTusker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class ProdigiousTusker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG33_430";
  public const string Text = "Whenever a friendly <b>Rally</b> minion attacks, this plays 2 <b>Blood Gems</b> on itself.";
  public const string GoldenText = "Whenever a friendly <b>Rally</b> minion attacks, this plays 4 <b>Blood Gems</b> on itself.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      if (!attacker.HasRally())
        return;
      for (int index = 0; index < this.DoubleIfGolden(2); ++index)
        this.Simulator.CastBloodGem((Minion) this, (Entity) this);
    });
  }
}
