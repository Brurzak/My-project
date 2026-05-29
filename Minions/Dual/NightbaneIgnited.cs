// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.NightbaneIgnited
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

public class NightbaneIgnited(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG29_815";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Give {0} different friendly minions this minion's Attack.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Give {0} different friendly minions this minion's Attack, twice.";

  public Action<Minion> GetDeathrattle() => NightbaneIgnited.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      int attackBuff = minion.attack();
      for (int index1 = 0; index1 < num; ++index1)
      {
        List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.CardID != "BG29_815")).ToList<Minion>();
        for (int index2 = 0; index2 < 2; ++index2)
        {
          Minion minion1;
          if (list.TryGetRandom<Minion>(out minion1))
          {
            minion1.IncreaseStats(attackBuff, 0, (Entity) minion);
            list.Remove(minion1);
          }
        }
      }
    });
  }
}
