// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.AridAtrocity
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class AridAtrocity(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG29_864";
  public const string Text = "<b>Deathrattle:</b> Summon a {0}/{1} Golem. <i>(Improved by each friendly minion type that died this combat!)</i>";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a {0}/{1} Golem. <i>(Improved by each friendly minion type that died this combat!)</i>";
  public const string SummonCardId = "BG29_864t";

  public Action<Minion> GetDeathrattle() => AridAtrocity.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> randomPerRace = (minion.ControlledByPlayer ? minion.Simulator.state.Player.DeadMinions : minion.Simulator.state.Opponent.DeadMinions).GetRandomPerRace();
      int num1 = (golden ? 10 : 5) * randomPerRace.Count<Minion>();
      List<Minion> minionList = minion.TrySummonMinion(new Summon("BG29_864t", golden));
      foreach (Minion minion1 in minionList)
      {
        int num2 = minionList[0].health();
        int num3 = minionList[0].attack();
        minionList[0].SetStats(new int?(num3 + num1), new int?(num2 + num1));
      }
    });
  }
}
