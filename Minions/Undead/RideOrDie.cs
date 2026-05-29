// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.RideOrDie
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class RideOrDie(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IPassiveAttackBonus,
  IEntity
{
  public const string CardId = "BG33_115";
  public const string Text = "<b>Reborn</b> Your other Undead have +1 Attack.";
  public const string GoldenText = "<b>Reborn</b> Your other Undead have +2 Attack.";

  public int PassiveAttackBonusFor(Minion minion)
  {
    return minion.IsUndead() && minion != this ? this.DoubleIfGolden(1) : 0;
  }
}
