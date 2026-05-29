// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.Zippers
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class Zippers(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_HERO_002_Buddy";
  public const string Text = "<b>Deathrattle:</b> Get a helpful card.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 helpful cards.";

  public Action<Minion> GetDeathrattle() => Zippers.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.AddCardToFriendlyHand((CardEntity) new UnknownCardEntity((Entity) minion, minion.Simulator));
      if (!golden)
        return;
      minion.AddCardToFriendlyHand((CardEntity) new UnknownCardEntity((Entity) minion, minion.Simulator));
    });
  }
}
