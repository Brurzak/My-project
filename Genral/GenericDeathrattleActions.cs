// Decompiled with JetBrains decompiler
// Type: BobsBuddy.GenericDeathrattleActions
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;

#nullable enable
namespace BobsBuddy;

public static class GenericDeathrattleActions
{
  public static readonly Action<Minion> SneedHeroPower = new Action<Minion>(GenericDeathrattles.SneedHeroPower);
  public static readonly Action<Minion> Plants = new Action<Minion>(GenericDeathrattles.Plants);
  public static readonly Action<Minion> EarthInvocationDeathrattle = new Action<Minion>(GenericDeathrattles.EarthInvocationDeathrattle);
  public static readonly Action<Minion> Crab = new Action<Minion>(GenericDeathrattles.Crab);
  public static readonly Action<Minion> CrabGolden = new Action<Minion>(GenericDeathrattles.CrabGolden);
}
