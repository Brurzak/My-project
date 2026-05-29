// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.DeterminedDefender
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class DeterminedDefender(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG35_122";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Give adjacent minions +{0}/+{1} and <b>Taunt</b>.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Give adjacent minions +{0}/+{1} and <b>Taunt</b>.";

  public Action<Minion> GetDeathrattle() => DeterminedDefender.Deathrattle(this.golden);

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
      List<Minion> minionList = new List<Minion>();
      if (minion1 != null && minion1.IsAlive())
        minionList.Add(minion1);
      if (rightNeighbor != null && rightNeighbor.IsAlive())
        minionList.Add(rightNeighbor);
      int num = golden ? 10 : 5;
      foreach (Minion minion2 in minionList)
      {
        minion2.IncreaseStats(num, num, (Entity) minion);
        minion2.taunt = true;
      }
    });
  }
}
