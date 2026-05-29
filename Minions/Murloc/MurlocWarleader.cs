// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.MurlocWarleader
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class MurlocWarleader(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IPassiveAttackBonus,
  IEntity
{
  public const string CardId = "BG_EX1_507";
  public const string Text = "Your other Murlocs have +2 Attack.";
  public const string GoldenText = "Your other Murlocs have +4 Attack.";

  public int PassiveAttackBonusFor(Minion minion)
  {
    return minion != this && minion.IsMurloc() ? this.DoubleIfGolden(2) : 0;
  }
}
