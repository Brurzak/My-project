// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.CultistSthara
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class CultistSthara(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG27_081";
  public const string Text = "<b><b>Stealth</b>.</b> <b>Deathrattle:</b> Summon your first Demon that died this combat with its maximum stats.";
  public const string GoldenText = "<b><b>Stealth</b>.</b> <b>Deathrattle:</b> Summon your first 2 Demons that died this combat with their maximum stats.";

  public Action<Minion> GetDeathrattle() => CultistSthara.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> list1 = (minion.FriendlySide == minion.Simulator.playerSide ? (IEnumerable<Minion>) minion.Simulator.state.Player.DeadMinions : (IEnumerable<Minion>) minion.Simulator.state.Opponent.DeadMinions).Where<Minion>((Func<Minion, bool>) (x => x.IsDemon())).ToList<Minion>();
      int count = Math.Min(golden ? 2 : 1, list1.Count);
      List<Summon> list2 = list1.Take<Minion>(count).Select<Minion, Summon>((Func<Minion, Summon>) (m => new Summon(m.CardID, m.golden)
      {
        SetStats = new (int, int)?((m.maxAttack, m.maxHealth))
      })).ToList<Summon>();
      minion.TrySummonMinions(list2);
    });
  }
}
