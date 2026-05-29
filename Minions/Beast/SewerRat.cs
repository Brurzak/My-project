// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SewerRat
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SewerRat(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG19_010";
  public const string Text = "<b>Deathrattle:</b> Summon a 2/3 Turtle with <b>Taunt</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a 4/6 Turtle with <b>Taunt</b>.";

  public Action<Minion> GetDeathrattle() => SewerRat.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      foreach (Minion minion1 in minion.TrySummonMinion(new Summon("BG19_010t", golden)))
        minion1.taunt = true;
    });
  }
}
