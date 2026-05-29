// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.TideRaiser
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Spells.TavernSpells;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class TideRaiser(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_920";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Cast Shifting Tide on an adjacent minion.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Cast Shifting Tide on adjacent minions.";
  private static ITavernSpell _shiftingTideSpell = (ITavernSpell) new ShiftingTideSpell();

  public Action<Minion> GetDeathrattle() => TideRaiser.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion minion1;
      Minion rightNeighbor;
      if (!minion.IsAlive())
      {
        (minion1, rightNeighbor) = minion.GetLastKnownLivingNeighbors();
      }
      else
      {
        Minion leftNeighbor = minion.GetLeftNeighbor();
        rightNeighbor = minion.GetRightNeighbor();
        minion1 = leftNeighbor;
      }
      List<Minion> list = new List<Minion>();
      if (minion1 != null && minion1.IsAlive())
        list.Add(minion1);
      if (rightNeighbor != null && rightNeighbor.IsAlive())
        list.Add(rightNeighbor);
      if (golden)
      {
        foreach (Minion target in list)
          minion.Simulator.CastTavernSpell(TideRaiser._shiftingTideSpell, (Entity) minion, target);
      }
      else
      {
        Minion target;
        if (!list.TryGetRandom<Minion>(out target))
          return;
        minion.Simulator.CastTavernSpell(TideRaiser._shiftingTideSpell, (Entity) minion, target);
      }
    });
  }
}
