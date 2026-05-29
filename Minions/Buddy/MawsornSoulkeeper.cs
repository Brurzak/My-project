// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Buddy.MawsornSoulkeeper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Buddy;

public class MawsornSoulkeeper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "TB_BaconShop_HERO_702_Buddy";
  public const string Text = "<b>Deathrattle:</b> Summon 2 random Undead.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon 4 random Undead.";

  private static bool HasRace(Card card, Race race)
  {
    return race == card.Race || race == card.SecondaryRace || race != 25 && (card.Race == 26 || card.SecondaryRace == 26);
  }

  public Action<Minion> GetDeathrattle() => MawsornSoulkeeper.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Summon> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(minion.ControlledByPlayer, (Race) 11).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>();
      minion.TrySummonRandomMinions(list, golden ? 4 : 2);
    });
  }
}
