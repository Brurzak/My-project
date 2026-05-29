// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.BloodsnoutWarlord
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class BloodsnoutWarlord(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG33_884";
  public const string Text = "Whenever a friendly <b>Rally</b> minion attacks, this plays {0} <b>Blood Gems</b> on all your minions.";
  public const string GoldenText = "Whenever a friendly <b>Rally</b> minion attacks, this plays {0} <b>Blood Gems</b> on all your minions.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion _)
  {
    return (Action) (() =>
    {
      if (!attacker.HasRally())
        return;
      int num = this.DoubleIfGolden(3);
      foreach (Minion target in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
      {
        for (int index = 0; index < num; ++index)
          this.Simulator.CastBloodGem(target, (Entity) this);
      }
    });
  }
}
