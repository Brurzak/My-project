// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.HatefulHag
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class HatefulHag(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG29_120";
  public const string Text = "<b>Rally:</b> Give the minion to the right of this <b>Reborn</b>.";
  public const string GoldenText = "<b>Rally:</b> Give the 2 minions to the right of this <b>Reborn</b>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (attacker =>
    {
      foreach (Minion rightNeighbor in this.GetRightNeighbors(this.DoubleIfGolden(1)))
        rightNeighbor.reborn = true;
    });
  }
}
