// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.AutoAssembler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class AutoAssembler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_172";
  public const string Text = "<b>Magnetic</b> <b>Deathrattle:</b> Summon an Ancestral Automaton.";
  public const string GoldenText = "<b>Magnetic</b> <b>Deathrattle:</b> Summon a Golden Ancestral Automaton.";

  public Action<Minion> GetDeathrattle() => AutoAssembler.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(new Summon("BG_TTN_401", golden)));
  }
}
