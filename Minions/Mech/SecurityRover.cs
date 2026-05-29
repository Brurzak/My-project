// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.SecurityRover
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class SecurityRover(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BOT_218";
  public const string Text = "Whenever this minion takes damage, summon a 2/3 Mech with <b>Taunt</b>.";
  public const string GoldenText = "Whenever this minion takes damage, summon a 4/6 Mech with <b>Taunt</b>.";

  public Action? OnTakeDamage(int value)
  {
    return (Action) (() => this.TrySummonMinion(new Summon("BOT_218t", this.golden)));
  }
}
