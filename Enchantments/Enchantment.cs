// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Enchantments.Enchantment
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Enchantments;

public class Enchantment(string cardId, Simulator simulator, bool controlledByPlayer) : Entity(cardId, simulator, controlledByPlayer)
{
  public Entity? AttachedTo { get; set; }

  public int ScriptDataNum1 { get; set; }

  public int ScriptDataNum2 { get; set; }

  public virtual Enchantment Clone(Simulator? simulator = null)
  {
    Enchantment enchantment = (simulator ?? this.Simulator).EnchantmentFactory.Create(this.CardID, this.ControlledByPlayer);
    enchantment.ScriptDataNum1 = this.ScriptDataNum1;
    enchantment.ScriptDataNum2 = this.ScriptDataNum2;
    return enchantment;
  }

  public override string ToString() => $"[{this.AttachedTo}: {this.GetType().Name} Enchantment]";
}
