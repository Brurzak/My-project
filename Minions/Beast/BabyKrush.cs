// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.BabyKrush
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class BabyKrush(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAttack,
  IEntity
{
  public const string CardId = "BG22_001";
  public const string Text = "Whenever this attacks, summon an 8/8 Devilsaur to attack the target first.";
  public const string GoldenText = "Whenever this attacks, summon a 16/16 Devilsaur to attack the target first.";

  public Action? OnAttack(Minion target)
  {
    return (Action) (() => this.TrySummonMinion(new Summon("BG22_001t2", this.golden)));
  }
}
