// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.ObsidianRavager
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class ObsidianRavager(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG27_017";
  public const string Text = "<b>Rally:</b> Deal damage equal to this minion's Attack to the target and an adjacent minion.";
  public const string GoldenText = "<b>Rally:</b> Deal damage equal to this minion's Attack to the target and its neighbors.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> source = new List<Minion>() { target };
      List<Minion> list = new List<Minion>()
      {
        target.GetLeftNeighbor(),
        target.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null)).Cast<Minion>().ToList<Minion>();
      if (isGolden)
      {
        source.AddRange((IEnumerable<Minion>) list);
      }
      else
      {
        Minion minion1;
        if (list.TryGetRandom<Minion>(out minion1))
          source.Add(minion1);
      }
      minion.Simulator.ProcessDamage(source.Select<Minion, Damage>((Func<Minion, Damage>) (x => new Damage(minion.attack(), x, (Entity) minion))));
    });
  }
}
