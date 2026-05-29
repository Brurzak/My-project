// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.HandlessForsaken
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class HandlessForsaken(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_010";
  public const string Text = "<b>Deathrattle:</b> Summon a 2/1 Hand with <b>Reborn</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two 2/1 Hands with <b>Reborn</b>.";

  public Action<Minion> GetDeathrattle() => HandlessForsaken.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinion(new Summon("BG25_010t"));
      if (!golden)
        return;
      minion.TrySummonMinion(new Summon("BG25_010t"));
    });
  }
}
