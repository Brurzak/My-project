// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.RadioStar
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class RadioStar(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_399";
  public const string Text = "<b>Deathrattle:</b> Get a plain copy of the minion that killed this.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 plain copies of the minion that killed this.";

  public Action<Minion> GetDeathrattle() => RadioStar.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (minion.KilledBy == null)
        return;
      minion.AddMinionToFriendlyHand(minion.KilledBy.CardID);
      if (!golden)
        return;
      minion.AddMinionToFriendlyHand(minion.KilledBy.CardID);
    });
  }
}
