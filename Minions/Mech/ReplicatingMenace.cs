// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.ReplicatingMenace
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class ReplicatingMenace(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_BOT_312";
  public const string Text = "<b>Magnetic</b> <b>Deathrattle:</b> Summon three 1/1 Microbots.";
  public const string GoldenText = "<b>Magnetic</b> <b>Deathrattle:</b> Summon three 2/2 Microbots.";

  public Action<Minion> GetDeathrattle() => ReplicatingMenace.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("BG_BOT_312t", golden), new Summon("BG_BOT_312t", golden), new Summon("BG_BOT_312t", golden)));
  }
}
