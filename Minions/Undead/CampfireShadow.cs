// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.CampfireShadow
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class CampfireShadow(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG33_113";
  public const string Text = "<b>Rally:</b> Get a plain copy of a random enemy minion.";
  public const string GoldenText = "<b>Rally:</b> Get 2 plain copies of a random enemy minion.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddCardToFriendlyHand((CardEntity) new RandomMinionCardEntity((Entity) minion, minion.Simulator));
      if (!isGolden)
        return;
      minion.AddCardToFriendlyHand((CardEntity) new RandomMinionCardEntity((Entity) minion, minion.Simulator));
    });
  }
}
