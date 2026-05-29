// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.SpiritOfAir
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class SpiritOfAir(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_76_Buddy";
  public const string Text = "<b>Deathrattle:</b> Give a random friendly minion <b>Windfury</b>, <b>Divine Shield</b>, and <b>Taunt</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Give 2 random friendly minions <b>Windfury</b>, <b>Divine Shield</b>, and <b>Taunt</b>.";

  public Action<Minion> GetDeathrattle() => SpiritOfAir.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion1;
        if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x =>
        {
          if (!x.IsAlive())
            return false;
          return !x.taunt || !x.hasDiv || !x.windfury;
        })).ToList<Minion>().TryGetRandom<Minion>(out minion1))
        {
          minion1.taunt = true;
          minion1.div = 1;
          minion1.windfury = true;
        }
      }
    });
  }
}
