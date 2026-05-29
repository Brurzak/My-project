// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.KarmicChameleon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class KarmicChameleon(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG31_802";
  public const string Text = "<b>Avenge (5):</b> Transform into a copy of the minion to the left of this.";
  public const string GoldenText = "<b>Avenge (5):</b> Transform into a Golden copy of the minion to the left of this.";

  public int AvengeRequirement => 5;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      Minion leftNeighbor = this.GetLeftNeighbor();
      if (leftNeighbor == null)
        return;
      int index = this.BoardPosition();
      Minion minion = leftNeighbor.Clone();
      minion.golden = this.golden;
      this.FriendlySide.Remove((Minion) this);
      this.FriendlySide.Insert(index, minion);
    });
  }
}
