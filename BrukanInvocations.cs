// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.BrukanInvocations
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.HeroPowers;

public static class BrukanInvocations
{
  public static void Earth(Simulator simulator, bool controlledByPlayer)
  {
    List<Minion> list = (controlledByPlayer ? (IEnumerable<Minion>) simulator.playerSide : (IEnumerable<Minion>) simulator.opponentSide).ToList<Minion>();
    Minion minion;
    for (int index = 0; index < 4 && list.TryGetRandom<Minion>(out minion); ++index)
    {
      minion.AdditionalDeathrattles.Add(new Action<Minion>(GenericDeathrattles.EarthInvocationDeathrattle));
      list.Remove(minion);
    }
  }

  public static void Fire(Simulator simulator, bool controlledByPlayer)
  {
    Minion minion = (controlledByPlayer ? (IEnumerable<Minion>) simulator.playerSide : (IEnumerable<Minion>) simulator.opponentSide).FirstOrDefault<Minion>((Func<Minion, bool>) (x => x.IsAlive()));
    minion?.IncreaseStats(minion.attack(), 0);
  }

  public static void Water(Simulator simulator, bool controlledByPlayer)
  {
    List<Minion> source = controlledByPlayer ? simulator.playerSide : simulator.opponentSide;
    if (source.Count == 0)
      return;
    Minion minion = source.Last<Minion>();
    minion.div = 1;
    minion.taunt = true;
  }

  public static void Lightning(Simulator simulator, bool controlledByPlayer)
  {
    List<Minion> source = controlledByPlayer ? simulator.opponentSide : simulator.playerSide;
    if (source.Count == 0)
      return;
    List<Damage> damageGroup = new List<Damage>();
    List<Minion> list = source.ToList<Minion>();
    Minion target;
    for (int index = 0; index < 5 && list.TryGetRandom<Minion>(out target); ++index)
    {
      damageGroup.Add(new Damage(1, target));
      list.Remove(target);
    }
    simulator.ProcessDamage((IEnumerable<Damage>) damageGroup);
  }
}
