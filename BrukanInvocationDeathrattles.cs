// Decompiled with JetBrains decompiler
// Type: BobsBuddy.HeroPowers.BrukanInvocationDeathrattles
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

#nullable enable
namespace BobsBuddy.HeroPowers;

public static class BrukanInvocationDeathrattles
{
  public static void Earth(Minion minion)
  {
    BrukanInvocations.Earth(minion.Simulator, minion.ControlledByPlayer);
  }

  public static void Fire(Minion minion)
  {
    BrukanInvocations.Fire(minion.Simulator, minion.ControlledByPlayer);
  }

  public static void Water(Minion minion)
  {
    BrukanInvocations.Water(minion.Simulator, minion.ControlledByPlayer);
  }

  public static void Lightning(Minion minion)
  {
    BrukanInvocations.Lightning(minion.Simulator, minion.ControlledByPlayer);
  }
}
