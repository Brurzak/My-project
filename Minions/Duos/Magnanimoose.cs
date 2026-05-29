// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Duos.Magnanimoose
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Duos;

public class Magnanimoose(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BGDUO_105";
  public const string Text = "<b>Deathrattle:</b> Summon a copy of a minion from your teammate's warband. Set its Health to 1 <i> (except Magnanimoose).</i>";
  public const string GoldenText = "<b>Deathrattle:</b> Summon copies of 2 minions from your teammate's warband. Set their Health to 1 <i> (except Magnanimoose).</i>";

  public Action<Minion> GetDeathrattle() => Magnanimoose.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      if (minion.TeammateSide == null)
        throw new UnsupportedInteractionException("Teammate's board is unknown", (Entity) minion);
      List<Minion> list1 = minion.TeammateSide.Where<Minion>((Func<Minion, bool>) (x => x.CardID != "BGDUO_105")).ToList<Minion>();
      GameState.PlayerState state = minion.ControlledByPlayer ? minion.Simulator.state.Player : minion.Simulator.state.Opponent;
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        List<Minion> list2 = list1.Where<Minion>((Func<Minion, bool>) (x => !state.SummonedByMoose.Any<Minion>((Func<Minion, bool>) (y => y.game_id == x.game_id)))).ToList<Minion>();
        Minion minion1;
        if (list2.Count > 0 && list2.TryGetRandom<Minion>(out minion1))
        {
          Minion minion2 = minion1.Clone(minion.Simulator);
          minion2.SetStats(new int?(minion2.attack()), new int?(1));
          if (minion.TrySummonMinion((Summon) minion2).Count > 0)
            state.SummonedByMoose.Add(minion1);
        }
      }
    });
  }
}
