// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.ElementiumSquirrelBomb
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class ElementiumSquirrelBomb(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_17_Buddy";
  public const string Text = "<b>Deathrattle:</b> Deal 4 damage to a random enemy minion for each of your Mechs that died this combat.";
  public const string GoldenText = "<b>Deathrattle:</b> Deal 8 damage to a random enemy minion for each of your Mechs that died this combat.";

  public Action<Minion> GetDeathrattle() => ElementiumSquirrelBomb.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = (minion.FriendlySide == minion.Simulator.playerSide ? (IEnumerable<Minion>) minion.Simulator.state.Player.DeadMinions : (IEnumerable<Minion>) minion.Simulator.state.Opponent.DeadMinions).Count<Minion>((Func<Minion, bool>) (x => x.IsMech()));
      int amount = golden ? 8 : 4;
      for (int index = 0; index < num; ++index)
      {
        Minion target;
        if (minion.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out target))
          minion.Simulator.ProcessDamage(amount, target, (Entity) minion);
      }
    });
  }
}
