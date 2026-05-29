// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.Pufferquil
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class Pufferquil(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterAnySpellCast,
  IEntity
{
  public const string CardId = "BG25_039";
  public const string Text = "Whenever a spell is cast on this, gain <b>Venomous</b> until next turn.";
  public const string GoldenText = "Whenever a spell is cast on this, gain <b>Venomous</b>.";

  public Action? OnAfterAnySpellCast(Entity? source, Minion? target)
  {
    return (Action) (() =>
    {
      if (target != this || this.IsDead() || this.venomous)
        return;
      this.venomous = true;
    });
  }
}
