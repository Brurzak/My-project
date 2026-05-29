// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.Photobomber
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class Photobomber(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnSetupPlayerStateCounters,
  IEntity,
  IDeathrattle
{
  public const string CardId = "BG34_780";
  public const string Text = "<b>Deathrattle:</b> Deal {0} damage to the highest-Health enemy minion. <i>(Improved by Tavern spells you've cast this game!)</i>";
  public const string GoldenText = "<b>Deathrattle:</b> Deal {0} damage to the highest-Health enemy minion twice. <i>(Improved by Tavern spells you've cast this game!)</i>";

  public void OnSetupPlayerStateCounters(GameState.PlayerState playerState)
  {
    playerState.TavernSpellCounter = this.ScriptDataNum1;
  }

  public Action<Minion> GetDeathrattle() => this.Deathrattle(this.golden);

  public Action<Minion> Deathrattle(bool isGolden)
  {
    return (Action<Minion>) (minion =>
    {
      int tavernSpellCounter = (minion.ControlledByPlayer ? minion.Simulator.state.Player : minion.Simulator.state.Opponent).TavernSpellCounter;
      for (int index = 0; index < (isGolden ? 2 : 1); ++index)
      {
        List<Minion> list = minion.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
        if (list.Count == 0)
          break;
        int highestHealth = list.Max<Minion>((Func<Minion, int>) (x => x.health()));
        Minion target;
        if (list.Where<Minion>((Func<Minion, bool>) (x => x.health() == highestHealth)).ToList<Minion>().TryGetRandom<Minion>(out target))
          minion.Simulator.ProcessDamage(tavernSpellCounter, target, (Entity) minion);
      }
    });
  }
}
