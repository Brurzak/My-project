// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.Mothership
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class Mothership(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG31_HERO_802pt7";
  public const string Text = "<b>Avenge (4):</b> Get a random Protoss minion.";
  public const string GoldenText = "<b>Avenge (4):</b> Get 2 random Protoss minions.";

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      this.AddCardToFriendlyHand((CardEntity) new RandomMinionCardEntity((Entity) this, this.Simulator));
      if (!this.golden)
        return;
      this.AddCardToFriendlyHand((CardEntity) new RandomMinionCardEntity((Entity) this, this.Simulator));
    });
  }
}
