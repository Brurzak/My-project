// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.ImpulsiveTrickster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class ImpulsiveTrickster(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG21_006";
  public const string Text = "<b>Deathrattle:</b> Give this minion's maximum Health to another friendly minion.";
  public const string GoldenText = "<b>Deathrattle:</b> Give this minion's maximum Health to another friendly minion, twice.";

  public Action<Minion> GetDeathrattle() => ImpulsiveTrickster.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      bool flag = minion.FriendlyEntities.Any<Entity>((Func<Entity, bool>) (x => x is ImpulsivePortrait));
      for (int index = 0; index < num; ++index)
      {
        if (flag)
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
          foreach (Minion minion2 in new List<Minion>()
          {
            minion1,
            rightNeighbor
          }.Where<Minion>((Func<Minion, bool>) (x => x != null && x.IsAlive())).Cast<Minion>().ToList<Minion>())
            minion2.IncreaseStats(0, minion.maxHealth);
        }
        else
        {
          Minion minion3;
          if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x != minion)).ToList<Minion>().TryGetRandom<Minion>(out minion3))
            minion3.IncreaseStats(0, minion.maxHealth);
        }
      }
    });
  }
}
