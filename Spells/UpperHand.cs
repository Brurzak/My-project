// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Spells.UpperHand
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Spells;

public class UpperHand(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Objective(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG28_573";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion minion;
      if (!this.OpposingSide.TryGetRandom<Minion>(out minion))
        return;
      minion.SetStats(new int?(), new int?(1));
    });
  }
}
