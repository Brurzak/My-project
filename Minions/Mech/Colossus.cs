// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.Colossus
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class Colossus(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAttack,
  IEntity
{
  public const string CardId = "BG31_HERO_802pt";
  public const string Text = "<b>Rally:</b> Deal {1} damage to the target's neighbors. <i>(Improves when you spend Gold!)</i>";
  public const string GoldenText = "<b>Rally:</b> Deal {1} damage to the target's neighbors. <i>(Improves when you spend Gold!)</i>";

  public Action? OnAttack(Minion target)
  {
    return (Action) (() =>
    {
      List<Minion> list = new List<Minion>()
      {
        target.GetLeftNeighbor(),
        target.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null && x.IsAlive())).Cast<Minion>().ToList<Minion>();
      List<Damage> damageGroup = new List<Damage>();
      foreach (Minion target1 in list)
        damageGroup.Add(new Damage(this.ScriptDataNum2, target1, (Entity) this));
      this.Simulator.ProcessDamage((IEnumerable<Damage>) damageGroup);
    });
  }
}
