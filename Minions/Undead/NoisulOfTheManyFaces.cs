// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.NoisulOfTheManyFaces
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class NoisulOfTheManyFaces(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionSummoned,
  IEntity
{
  public const string CardId = "BG32_325";
  public const string Text = "Whenever you summon an Undead, give all your Undead +{0}/+{1} permanently.";
  public const string GoldenText = "Whenever you summon an Undead, give all your Undead +{0}/+{1} permanently.";

  public Action OnFriendlyMinionSummoned(Minion summoned, Entity? source)
  {
    return (Action) (() =>
    {
      List<Minion> source1 = this.ControlledByPlayer ? this.Simulator.playerSide : this.Simulator.opponentSide;
      if (!source1.Any<Minion>())
        return;
      int atkBuff = this.DoubleIfGolden(3);
      int healthBuff = this.DoubleIfGolden(2);
      source1.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsUndead())).ToList<Minion>().ForEach((Action<Minion>) (minion => minion.IncreaseStats(atkBuff, healthBuff)));
    });
  }
}
