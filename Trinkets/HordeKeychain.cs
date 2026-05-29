// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.HordeKeychain
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Trinkets;

public class HordeKeychain(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus
{
  public const string CardId = "BG30_MagicItem_843t";
  private const int AttackBonus = 7;
  private const int HealthBonus = 5;

  private static bool IsMinionLowTier(Minion minion) => minion.tier <= 3;

  public int PassiveAttackBonusFor(Minion minion) => !HordeKeychain.IsMinionLowTier(minion) ? 0 : 7;

  public int PassiveHealthBonusFor(Minion minion) => !HordeKeychain.IsMinionLowTier(minion) ? 0 : 5;
}
