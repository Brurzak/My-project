// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.VolumizedEnchantment
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Enchantments;

public class VolumizedEnchantment(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus
{
  public const string CardId = "BG34_170e";

  public int PassiveAttackBonusFor(Minion target)
  {
    return target != this.AttachedTo ? 0 : this.ScriptDataNum1;
  }

  public int PassiveHealthBonusFor(Minion target)
  {
    return target != this.AttachedTo ? 0 : this.ScriptDataNum2;
  }
}
