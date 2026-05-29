// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.Untameabull
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class Untameabull(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG29_878";
  public const string Text = "Whenever this takes damage, gain <b>Divine Shield</b>.";
  public const string GoldenText = "Whenever this takes damage, gain <b>Divine Shield</b>.";

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() =>
    {
      if (this.IsDead())
        return;
      this.div = 1;
    });
  }
}
