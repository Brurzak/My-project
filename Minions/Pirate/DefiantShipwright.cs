// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.DefiantShipwright
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class DefiantShipwright(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionBuffed,
  IEntity
{
  public const string CardId = "BG21_018";
  public const string Text = "Whenever this gains Attack from other sources, gain +{1} Health.[x]Whenever this gains Attack from other sources, gain +{0}/+{1}.";
  public const string GoldenText = "Whenever this gains Attack from other sources, gain +{1} Health twice.[x]Whenever this gains Attack from other sources, gain +{0}/+{1} twice.";

  public Action? OnFriendlyMinionBuffed(
    Minion buffed,
    int attackChange,
    int healthChange,
    Entity? source)
  {
    return (Action) (() =>
    {
      if (buffed != this || attackChange <= 0 || source == this || !this.IsAlive())
        return;
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
        this.IncreaseStats(0, 1, (Entity) this);
    });
  }
}
