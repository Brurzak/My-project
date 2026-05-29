// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.Spacefarer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class Spacefarer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG31_820";
  public const string Text = "Whenever another friendly Pirate gains Attack, gain +2 Health.";
  public const string GoldenText = "Whenever another friendly Pirate gains Attack, gain +4 Health.";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (!buffed.IsPirate() || buffed == this || attackChange <= 0)
        return;
      this.IncreaseStats(0, this.DoubleIfGolden(2), (Entity) this);
    });
  }
}
