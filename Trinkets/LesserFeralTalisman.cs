// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.LesserFeralTalisman
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Trinkets;

public class LesserFeralTalisman(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus
{
  public const string CardId = "BG30_MagicItem_880";
  private const int AttackBonus = 2;
  private const int HealthBonus = 1;

  public int PassiveAttackBonusFor(Minion minion) => 2;

  public int PassiveHealthBonusFor(Minion minion) => 1;
}
