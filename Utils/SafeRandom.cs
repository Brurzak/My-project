// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Utils.SafeRandom
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable enable
namespace BobsBuddy.Utils;

public static class SafeRandom
{
  private static int _seedOffset = 0;
  private static int? _nextBaseSeed;
  private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>((Func<Random>) (() => new Random(SafeRandom.Seed)));

  private static int Seed
  {
    get => SafeRandom.GetBaseSeed() + Interlocked.Increment(ref SafeRandom._seedOffset);
  }

  internal static void ForceNextBaseSeed(int seed) => SafeRandom._nextBaseSeed = new int?(seed);

  private static int GetBaseSeed()
  {
    if (!SafeRandom._nextBaseSeed.HasValue)
      return Environment.TickCount;
    int baseSeed = SafeRandom._nextBaseSeed.Value;
    SafeRandom._nextBaseSeed = new int?();
    return baseSeed;
  }

  public static int Next() => SafeRandom._random.Value.Next();

  public static int Next(int minValue, int maxValue)
  {
    return SafeRandom._random.Value.Next(minValue, maxValue);
  }

  public static bool NextBool() => SafeRandom._random.Value.Next(2) > 0;

  [Obsolete("Unsafe. Use TryGetRandom instead.")]
  public static T GetRandom<T>(this List<T> list)
  {
    return list[list.Count > 1 ? SafeRandom.Next(0, list.Count) : 0];
  }

  public static bool TryGetRandom<T>(this List<T> list, out T item)
  {
    if (list.Count == 0)
    {
      item = default (T);
      return false;
    }
    item = list[list.Count > 1 ? SafeRandom.Next(0, list.Count) : 0];
    return true;
  }

  public static bool TryGetRandomIndex<T>(this List<T> list, out int index)
  {
    if (list.Count == 0)
    {
      index = -1;
      return false;
    }
    index = list.Count > 1 ? SafeRandom.Next(0, list.Count) : 0;
    return true;
  }

  public static List<T> GetRandomElements<T>(this List<T> list, int count)
  {
    List<T> list1 = list.ToList<T>();
    List<T> randomElements = new List<T>();
    T obj;
    for (int index = 0; index < count && list1.TryGetRandom<T>(out obj); ++index)
    {
      randomElements.Add(obj);
      list1.Remove(obj);
    }
    return randomElements;
  }

  public static List<Minion> GetRandomPerRace(this List<Minion> minions)
  {
    List<Minion> targetMinions = minions.Where<Minion>((Func<Minion, bool>) (x => x.PrimaryRace == 26 || x.SecondaryRace == 26)).ToList<Minion>();
    HashSet<Race> source = new HashSet<Race>(minions.SelectMany<Minion, Race>((Func<Minion, IEnumerable<Race>>) (x => (IEnumerable<Race>) new Race[2]
    {
      x.PrimaryRace,
      x.SecondaryRace
    })).Where<Race>((Func<Race, bool>) (x => x != null && x != 25 && x != 26)));
    while (source.Count > 0)
    {
      var data = source.Select(race => new
      {
        Race = race,
        Minions = minions.Where<Minion>((Func<Minion, bool>) (m => m.HasRace(race) && !targetMinions.Contains(m))).ToList<Minion>()
      }).OrderBy(x => x.Minions.Count).First();
      source.Remove(data.Race);
      Minion minion;
      if (data.Minions.TryGetRandom<Minion>(out minion))
        targetMinions.Add(minion);
    }
    return targetMinions;
  }
}
