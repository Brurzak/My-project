// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.SoreLoser
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class SoreLoser(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IPassiveAttackBonus,
  IEntity
{
  public const string CardId = "BG27_030";
  public const string Text = "Your other Undead have extra Attack equal to your Tier.";
  public const string GoldenText = "Your other Undead have extra Attack equal to double your Tier.";

  public int PassiveAttackBonusFor(Minion minion)
  {
    return minion.IsUndead() && minion != this ? this.DoubleIfGolden(this.ControlledByPlayer ? this.Simulator.PlayerInput.Tier : this.Simulator.PlayerInput.Tier) : 0;
  }
}
