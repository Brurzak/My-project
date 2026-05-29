// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.GreenVolumizerEnchantment
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Enchantments;

public class GreenVolumizerEnchantment(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Enchantment(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus
{
  public const string CardId = "BG34_170t3e";

  private int AtkBuff
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.VolumizerAtkBuff : this.Simulator.state.Player.VolumizerAtkBuff;
    }
  }

  private int HealthBuff
  {
    get
    {
      return !this.ControlledByPlayer ? this.Simulator.state.Opponent.VolumizerHealthBuff : this.Simulator.state.Player.VolumizerHealthBuff;
    }
  }

  public int PassiveAttackBonusFor(Minion target)
  {
    return target == this.AttachedTo ? this.ScriptDataNum1 + this.AtkBuff : 0;
  }

  public int PassiveHealthBonusFor(Minion target)
  {
    return target == this.AttachedTo ? this.ScriptDataNum2 + this.HealthBuff : 0;
  }
}
