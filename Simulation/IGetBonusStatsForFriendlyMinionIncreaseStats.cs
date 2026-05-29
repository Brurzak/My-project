// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Simulation.IGetBonusStatsForFriendlyMinionIncreaseStats
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

#nullable enable
namespace BobsBuddy.Simulation;

public interface IGetBonusStatsForFriendlyMinionIncreaseStats : IEntity
{
  (int, int)? GetBonusStatsForFriendlyMinionIncreaseStats(
    Minion minion,
    Entity? source,
    int attackIncrease,
    int healthIncrease);
}
