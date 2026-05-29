// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SlyRaptor
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
namespace BobsBuddy.Minions.Beast;

public class SlyRaptor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_806";
  public const string Text = "<b>Deathrattle:</b> Summon a random Beast. Set its stats to {0}/{1}.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a random Beast. Set its stats to {0}/{1}.";

  public Action<Minion> GetDeathrattle() => SlyRaptor.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Summon> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(minion.ControlledByPlayer, (Race) 20).Where<Card>((Func<Card, bool>) (x => x.Id != minion.CardID)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>();
      int stats = golden ? 12 : 6;
      minion.TrySummonRandomMinions(list.Select<Summon, Summon>((Func<Summon, Summon>) (x =>
      {
        x.SetStats = new (int, int)?((stats, stats));
        return x;
      })).ToList<Summon>(), 1);
    });
  }
}
