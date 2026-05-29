// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.RylakMetalhead
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class RylakMetalhead(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_801";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Trigger the <b>Battlecry</b> of an adjacent minion.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Trigger the <b>Battlecries</b> of adjacent minions.";

  public Action<Minion> GetDeathrattle() => RylakMetalhead.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion minion1;
      Minion rightNeighbor;
      if (!minion.IsAlive())
      {
        (minion1, rightNeighbor) = minion.LastKnownNeighbors;
      }
      else
      {
        Minion leftNeighbor = minion.GetLeftNeighbor();
        rightNeighbor = minion.GetRightNeighbor();
        minion1 = leftNeighbor;
      }
      List<Minion> list = new List<Minion>();
      if (minion1 != null && minion1.GetTriggers<IBattlecry>().Any<IBattlecry>() && minion1.IsAlive())
        list.Add(minion1);
      if (rightNeighbor != null && rightNeighbor.GetTriggers<IBattlecry>().Any<IBattlecry>() && rightNeighbor.IsAlive())
        list.Add(rightNeighbor);
      if (golden)
      {
        foreach (Minion target in list)
          minion.Simulator.InvokeBattlecry(target);
      }
      else
      {
        Minion target;
        if (!list.TryGetRandom<Minion>(out target))
          return;
        minion.Simulator.InvokeBattlecry(target);
      }
    });
  }
}
