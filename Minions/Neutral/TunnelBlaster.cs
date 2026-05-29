// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.TunnelBlaster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class TunnelBlaster(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_DAL_775";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Deal 3 damage to all minions.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Deal 3 damage to all minions twice.";

  public Action<Minion> GetDeathrattle() => TunnelBlaster.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
        minion.Simulator.ProcessDamage(minion.FriendlySide.Concat<Minion>((IEnumerable<Minion>) minion.OpposingSide).Select<Minion, Damage>((Func<Minion, Damage>) (x => new Damage(3, x, (Entity) minion))));
    });
  }
}
