// Decompiled with JetBrains decompiler
// Type: BobsBuddy.GenericDeathrattles
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy;

public static class GenericDeathrattles
{
  private static readonly Dictionary<int, List<Summon>> _sneedDeathrattleByTier = new Dictionary<int, List<Summon>>()
  {
    [1] = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 1)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>(),
    [2] = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 2)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>(),
    [3] = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 3)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>(),
    [4] = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 4)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>(),
    [5] = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 5)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>(),
    [6] = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.TechLevel == 6)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>()
  };

  public static void SneedHeroPower(Minion minion)
  {
    int key = Math.Max(1, minion.tier - 1);
    List<Summon> options;
    if (!GenericDeathrattles._sneedDeathrattleByTier.TryGetValue(key, out options))
      return;
    minion.TrySummonRandomMinions(options, 1);
  }

  public static void Plants(Minion minion)
  {
    minion.TrySummonMinions(new List<Summon>()
    {
      (Summon) "UNG_999t2t1",
      (Summon) "UNG_999t2t1"
    });
  }

  public static void EarthInvocationDeathrattle(Minion minion)
  {
    minion.TrySummonMinion((Summon) "BG22_HERO_001p_t1et");
  }

  public static void Crab(Minion minion) => minion.TrySummonMinion((Summon) "BG27_004t2");

  public static void CrabGolden(Minion minion) => minion.TrySummonMinion((Summon) "BG27_004_Gt2");
}
