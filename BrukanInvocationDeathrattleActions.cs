// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.BrukanInvocationDeathrattleActions
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using System;

#nullable enable
namespace BobsBuddy.HeroPowers;

public static class BrukanInvocationDeathrattleActions
{
  public static readonly Action<Minion> Earth = new Action<Minion>(BrukanInvocationDeathrattles.Earth);
  public static readonly Action<Minion> Fire = new Action<Minion>(BrukanInvocationDeathrattles.Fire);
  public static readonly Action<Minion> Water = new Action<Minion>(BrukanInvocationDeathrattles.Water);
  public static readonly Action<Minion> Lightning = new Action<Minion>(BrukanInvocationDeathrattles.Lightning);
}
