// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.CoilskarSapper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class CoilskarSapper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_836";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Get 2 random <b>Spellcraft</b> spells.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Get 4 random <b>Spellcraft</b> spells.";

  public Action<Minion> GetDeathrattle() => CoilskarSapper.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 4 : 2;
      for (int index = 0; index < num; ++index)
        minion.AddSpellToFriendlyHand();
    });
  }
}
