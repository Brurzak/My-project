// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.FlagbearerPortrait
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;

#nullable enable
namespace BobsBuddy.Trinkets;

public class FlagbearerPortrait(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IPassiveAttackBonus,
  IEntity
{
  public const string CardId = "BG30_MagicItem_921";

  public int PassiveAttackBonusFor(Minion target) => !(target.CardID == "BGS_061t") ? 0 : 6;
}
