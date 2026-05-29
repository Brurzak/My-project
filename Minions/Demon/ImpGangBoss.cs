// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.ImpGangBoss
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class ImpGangBoss(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG_BRM_006";
  public const string Text = "Whenever this minion takes damage, summon a 1/1 Imp.";
  public const string GoldenText = "Whenever this minion takes damage, summon a 2/2 Imp.";

  public Action? OnTakeDamage(int value)
  {
    return (Action) (() => this.TrySummonMinion(new Summon("BG_BRM_006t", this.golden)));
  }
}
