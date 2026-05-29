// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.TimewarpedRiplash
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class TimewarpedRiplash(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_325";
  public const string Text = "<b>Deathrattle:</b> Get a copy of the last Tavern spell you cast.";
  public const string GoldenText = "<b>Deathrattle:</b> Get 2 copies of the last Tavern spell you cast.";

  public Action<Minion> GetDeathrattle() => TimewarpedRiplash.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
        minion.AddSpellToFriendlyHand();
    });
  }
}
