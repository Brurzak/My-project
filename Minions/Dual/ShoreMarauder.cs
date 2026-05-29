// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.ShoreMarauder
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class ShoreMarauder(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IGetBonusStatsForFriendlyMinionIncreaseStats,
  IEntity
{
  public const string CardId = "BG34_502";
  public const string Text = "Your Pirates and Elementals give an extra +{0}/+{1}.";
  public const string GoldenText = "Your Pirates and Elementals give an extra +{0}/+{1}.";

  public (int, int)? GetBonusStatsForFriendlyMinionIncreaseStats(
    Minion minion,
    Entity? source,
    int attackIncrease,
    int healthIncrease)
  {
    return source is Minion minion1 && (minion1.IsElemental() || minion1.IsPirate()) ? new (int, int)?((this.DoubleIfGolden(1), this.DoubleIfGolden(1))) : new (int, int)?();
  }
}
