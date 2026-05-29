// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TimewarpedTraveler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class TimewarpedTraveler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_Giant_605";
  public const string Text = "<b>Avenge ({0}):</b> Get a random 1-Cost card from the Minor <b>Timewarp</b>.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Get two random 1-Cost cards from the Minor <b>Timewarp</b>.";

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      this.AddCardToFriendlyHand(new CardEntity("Card", (Entity) this, this.Simulator));
      if (!this.golden)
        return;
      this.AddCardToFriendlyHand(new CardEntity("Card", (Entity) this, this.Simulator));
    });
  }
}
