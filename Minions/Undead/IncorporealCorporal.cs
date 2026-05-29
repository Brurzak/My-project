// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.IncorporealCorporal
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class IncorporealCorporal(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterAttack,
  IEntity
{
  public const string CardId = "BG26_RLK_117";
  public const string Text = "After this minion attacks, destroy it.";
  public const string GoldenText = "After this minion attacks, destroy it.";

  public Action? OnAfterAttack(Minion target)
  {
    return (Action) (() => this.Simulator.Destroy(this.AttachedOrThis, (Entity) this));
  }
}
