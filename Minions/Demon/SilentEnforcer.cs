// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.SilentEnforcer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class SilentEnforcer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG33_156";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Deal {0} damage to all minions <i>(except friendly Demons)</i>.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Deal {0} damage to all minions twice <i>(except friendly Demons)</i>.";

  public Action<Minion> GetDeathrattle() => SilentEnforcer.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      for (int index = 0; index < (golden ? 2 : 1); ++index)
      {
        foreach (Minion target in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => !x.IsDemon())).ToList<Minion>().Concat<Minion>((IEnumerable<Minion>) minion.OpposingSide).Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
          minion.Simulator.ProcessDamage(2, target, (Entity) minion);
      }
    });
  }
}
