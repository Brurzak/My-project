// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.KangorsApprentice
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class KangorsApprentice(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BGS_012";
  public const string Text = "<b>Deathrattle:</b> Summon plain copies of your first 2 Mechs that died this combat.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon plain copies of your first 4 Mechs that died this combat.";

  public Action<Minion> GetDeathrattle() => KangorsApprentice.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> deadMechs = (minion.FriendlySide == minion.Simulator.playerSide ? minion.Simulator.state.Player.DeadMinions : minion.Simulator.state.Opponent.DeadMinions).Where<Minion>((Func<Minion, bool>) (x => x.IsMech())).ToList<Minion>();
      List<Summon> list = Enumerable.Range(0, Math.Min(golden ? 4 : 2, deadMechs.Count)).Select<int, Summon>((Func<int, Summon>) (i => new Summon(deadMechs[i].CardID, deadMechs[i].golden))).ToList<Summon>();
      minion.TrySummonMinions(list);
    });
  }
}
