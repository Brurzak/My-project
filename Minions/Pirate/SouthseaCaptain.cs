// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.SouthseaCaptain
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class SouthseaCaptain(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus
{
  public const string CardId = "BG_NEW1_027";
  public const string Text = "Your other Pirates have +1/+1.";
  public const string GoldenText = "Your other Pirates have +2/+2.";

  public int PassiveAttackBonusFor(Minion minion)
  {
    return minion != this && minion.IsPirate() ? this.DoubleIfGolden(1) : 0;
  }

  public int PassiveHealthBonusFor(Minion minion)
  {
    return minion != this && minion.IsPirate() ? this.DoubleIfGolden(1) : 0;
  }
}
