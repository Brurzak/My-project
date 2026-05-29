// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.TimewarpedSwirler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class TimewarpedSwirler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IGetBonusStatsForFriendlyMinionIncreaseStats,
  IEntity
{
  public const string CardId = "BG34_Giant_686";
  public const string Text = "Your Elementals give an extra +{0}/+{1} this game.";
  public const string GoldenText = "Your Elementals give an extra +{0}/+{1} this game.";

  public (int, int)? GetBonusStatsForFriendlyMinionIncreaseStats(
    Minion minion,
    Entity? source,
    int attackIncrease,
    int healthIncrease)
  {
    return source is Minion minion1 && minion1.IsElemental() ? new (int, int)?((this.DoubleIfGolden(3), this.DoubleIfGolden(3))) : new (int, int)?();
  }
}
