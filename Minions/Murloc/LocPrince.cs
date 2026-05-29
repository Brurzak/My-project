// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.LocPrince
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class LocPrince(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IGetBonusStatsForFriendlyMinionIncreaseStats,
  IEntity
{
  public const string CardId = "BG29_889";
  public const string Text = "<b>Divine Shield</b> Whenever this gains stats, add +2/+1 to that amount <i>(wherever this is)</i>.";
  public const string GoldenText = "<b>Divine Shield</b> Whenever this gains stats, add +4/+2 to that amount <i>(wherever this is)</i>.";

  public (int, int)? GetBonusStatsForFriendlyMinionIncreaseStats(
    Minion minion,
    Entity? source,
    int attackIncrease,
    int healthIncrease)
  {
    return minion != this ? new (int, int)?() : new (int, int)?((this.DoubleIfGolden(2), this.DoubleIfGolden(1)));
  }
}
