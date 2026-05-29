// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SilverGoose
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SilverGoose(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG29_801";
  public const string Text = "Whenever this takes damage, summon a 2/2 Fledgling with <b>Taunt</b>.";
  public const string GoldenText = "Whenever this takes damage, summon a 4/4 Fledgling with <b>Taunt</b>.";

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() => this.TrySummonMinion(new Summon("BG29_801t", this.golden)));
  }
}
