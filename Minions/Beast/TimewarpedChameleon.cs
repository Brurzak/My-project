// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedChameleon
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedChameleon(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG34_Giant_042";
  public const string Text = "<b>Start of Combat:</b> Transform into a copy of the minion to the left of this.";
  public const string GoldenText = "<b>Start of Combat:</b> Transform into a Golden copy of the minion to the left of this.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      Minion leftNeighbor = this.GetLeftNeighbor();
      if (leftNeighbor == null)
        return;
      int index = this.BoardPosition();
      Minion minion = leftNeighbor.CloneAsShallowExactCopy();
      if (this.golden && minion.CanBeMadeGolden())
        minion.golden = true;
      this.FriendlySide.Remove((Minion) this);
      this.FriendlySide.Insert(index, minion);
    });
  }
}
