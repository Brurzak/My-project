// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SpikedSavior
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SpikedSavior(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG29_808";
  public const string Text = "<b><b>Taunt</b>. <b>Reborn</b>.</b> <b>Deathrattle:</b> Give your minions +1 Health and deal 1 damage to them.";
  public const string GoldenText = "<b><b>Taunt</b>. <b>Reborn</b>.</b> <b>Deathrattle:</b> Give your minions +1 Health and deal 1 damage to them, twice.";

  public Action<Minion> GetDeathrattle() => SpikedSavior.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>();
        if (list.Count == 0)
          break;
        foreach (Minion minion1 in list)
          minion1.IncreaseStats(0, 1);
        minion.Simulator.ProcessDamage(list.Select<Minion, Damage>((Func<Minion, Damage>) (x => new Damage(1, x, (Entity) minion))));
      }
    });
  }
}
