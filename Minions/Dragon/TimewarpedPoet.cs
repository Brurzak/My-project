// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TimewarpedPoet
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TimewarpedPoet(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IGetBonusStatsForFriendlyMinionIncreaseStats,
  IEntity
{
  public const string CardId = "BG34_Giant_314";
  public const string Text = "<b>Divine Shield</b> All your Dragons keep <b><b>Bonus Keyword</b>s</b> and stats gained in combat.";
  public const string GoldenText = "<b>Divine Shield</b> All your Dragons keep <b><b>Bonus Keyword</b>s</b> and double stats gained in combat.";

  public (int, int)? GetBonusStatsForFriendlyMinionIncreaseStats(
    Minion minion,
    Entity? source,
    int attackIncrease,
    int healthIncrease)
  {
    if (!minion.IsDragon())
      return new (int, int)?();
    return !this.golden ? new (int, int)?() : new (int, int)?((attackIncrease, healthIncrease));
  }
}
