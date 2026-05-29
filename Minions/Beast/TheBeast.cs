// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TheBeast
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TheBeast(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "EX1_577";
  public const string Text = "<b>Deathrattle:</b> Summon a 3/3 Pip Quickwit for your opponent.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a 3/3 Pip Quickwit for your opponent.";

  public Action<Minion> GetDeathrattle() => TheBeast.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.Simulator.TrySummonMinion(new Summon("EX1_finkle", golden), minion.OpposingSide, minion.OpposingSide.Count, (Entity) minion));
  }
}
