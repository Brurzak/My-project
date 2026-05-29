// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.TimewarpedChimera
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class TimewarpedChimera(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BG34_Giant_679";
  public const string Text = "Whenever this takes damage, give a friendly minion of each type +{0}/+{1} permanently.";
  public const string GoldenText = "Whenever this takes damage, give a friendly minion of each type +{0}/+{1} permanently, twice.";

  public Action? OnTakeDamage(int amount)
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.GetRandomPerRace())
      {
        minion.IncreaseStats(2, 1, (Entity) this);
        if (this.golden)
          minion.IncreaseStats(2, 1, (Entity) this);
      }
    });
  }
}
