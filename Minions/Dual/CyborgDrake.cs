// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.CyborgDrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class CyborgDrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IPassiveAttackBonus,
  IEntity
{
  public const string CardId = "BG25_043";
  public const string Text = "<b>Divine Shield</b> Your minions with <b>Divine Shield</b> have +6 Attack.";
  public const string GoldenText = "<b>Divine Shield</b> Your minions with <b>Divine Shield</b> have +12 Attack.";

  public int PassiveAttackBonusFor(Minion target) => target.hasDiv ? this.DoubleIfGolden(6) : 0;
}
