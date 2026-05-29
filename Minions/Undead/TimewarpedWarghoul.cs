// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TimewarpedWarghoul
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TimewarpedWarghoul(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_331";
  public const string Text = "<b>Taunt.</b> <b>Deathrattle:</b> Trigger an adjacent minion's <b>Deathrattle</b> <i>(except Timewarped Warghoul)</i>.";
  public const string GoldenText = "<b>Taunt.</b> <b>Deathrattle:</b> Trigger adjacent minions' <b>Deathrattles</b> <i>(except Timewarped Warghoul)</i>.";

  public Action<Minion> GetDeathrattle() => TimewarpedWarghoul.Deathrattle(this.golden);

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
      if (minion1 != null && minion1.HasDeathrattle() && minion1.IsAlive() && minion1.CardID != "BG34_Giant_331")
        list.Add(minion1);
      if (rightNeighbor != null && rightNeighbor.HasDeathrattle() && rightNeighbor.IsAlive() && rightNeighbor.CardID != "BG34_Giant_331")
        list.Add(rightNeighbor);
      if (golden)
      {
        foreach (Minion minion2 in list)
          minion.Simulator.TriggerDeathrattles(minion2);
      }
      else
      {
        Minion minion3;
        if (!list.TryGetRandom<Minion>(out minion3))
          return;
        minion.Simulator.TriggerDeathrattles(minion3);
      }
    });
  }
}
