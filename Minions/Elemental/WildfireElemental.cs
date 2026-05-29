// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.WildfireElemental
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class WildfireElemental(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterAttack,
  IEntity
{
  public const string CardId = "BGS_126";
  public const string Text = "After this attacks and kills a minion, deal excess damage to an adjacent minion.";
  public const string GoldenText = "After this attacks and kills a minion, deal excess damage to both adjacent minions.";

  public Action? OnAfterAttack(Minion target)
  {
    return (Action) (() =>
    {
      int overkill = -target.health();
      if (overkill <= 0)
        return;
      List<Minion> list = new List<Minion>()
      {
        target.GetLeftNeighbor(),
        target.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null)).Cast<Minion>().ToList<Minion>();
      if (this.golden)
      {
        this.Simulator.ProcessDamage(list.Select<Minion, Damage>((Func<Minion, Damage>) (x => new Damage(overkill, x, (Entity) this))));
      }
      else
      {
        Minion target1;
        if (!list.TryGetRandom<Minion>(out target1))
          return;
        this.Simulator.ProcessDamage(overkill, target1, (Entity) this);
      }
    });
  }
}
