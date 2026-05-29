// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.HauntedCarapace
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Spells;

public class HauntedCarapace(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity,
  IPassiveHealthBonus
{
  public const string CardId = "BG33_112t";

  public int PassiveAttackBonusFor(Minion minion) => this.ScriptDataNum1;

  public int PassiveHealthBonusFor(Minion minion) => this.ScriptDataNum2;
}
