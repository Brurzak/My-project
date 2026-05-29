// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.MechanoTank
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class MechanoTank(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG21_023";
  public const string Text = "<b>Avenge (2):</b> Deal 5 damage to the highest-Health enemy minion.";
  public const string GoldenText = "<b>Avenge (2):</b> Deal 5 damage to the highest-Health enemy minion, twice.";

  public int AvengeRequirement => 2;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      for (int index = 0; index < this.DoubleIfGolden(1); ++index)
      {
        IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>> source = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).Select(x => new
        {
          Health = x.health(),
          Minion = x
        }).GroupBy(x => x.Health).OrderByDescending<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>, int>(x => x.Key).FirstOrDefault<IGrouping<int, \u003C\u003Ef__AnonymousType2<int, Minion>>>();
        var data;
        if (source != null && source.ToList().TryGetRandom(out data))
          this.Simulator.ProcessDamage(5, data.Minion, (Entity) this);
      }
    });
  }
}
