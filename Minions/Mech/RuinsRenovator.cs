// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.RuinsRenovator
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using HearthDb;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class RuinsRenovator(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG33_802";
  public const string Text = "<b>Divine Shield</b> <b>Deathrattle:</b> Summon a random <b>Divine Shield</b> minion.";
  public const string GoldenText = "<b>Divine Shield</b> <b>Deathrattle:</b> Summon 2 random <b>Divine Shield</b> minions.";
  private List<Summon> _summons = new List<Summon>();

  public Action<Minion> GetDeathrattle() => RuinsRenovator.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Card> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayer(minion.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Id != "BG33_802" && x.DivineShield)).ToList<Card>();
      Card card;
      if (list.TryGetRandom<Card>(out card))
        minion.TrySummonMinion((Summon) card);
      if (!golden || !list.TryGetRandom<Card>(out Card _))
        return;
      minion.TrySummonMinion((Summon) card);
    });
  }
}
