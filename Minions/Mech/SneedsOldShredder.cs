// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.SneedsOldShredder
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class SneedsOldShredder(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  private static readonly List<Summon> _deathRattleOptions = Cards.BaconPoolMinions.Values.Where<Card>((Func<Card, bool>) (x => x.Rarity == 5 && x.Entity.CardId != "BGS_006")).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>();
  public const string CardId = "BGS_006";
  public const string Text = "<b>Deathrattle:</b> Summon a random <b>Legendary</b> minion.";

  public Action<Minion> GetDeathrattle() => SneedsOldShredder.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonRandomMinions(SneedsOldShredder._deathRattleOptions, golden ? 2 : 1));
  }
}
