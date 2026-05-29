// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.Ghastcoiler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class Ghastcoiler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BGS_008";
  public const string Text = "<b>Deathrattle:</b> Summon 2 random <b>Deathrattle</b> minions.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon 4 random <b>Deathrattle</b> minions.";

  public Action<Minion> GetDeathrattle() => Ghastcoiler.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Summon> list = minion.Simulator.MinionFactory.MinionPoolOptionsPlayer(minion.ControlledByPlayer).Where<Card>((Func<Card, bool>) (x => x.Id != minion.CardID && x.Deathrattle)).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>();
      minion.TrySummonRandomMinions(list, golden ? 4 : 2);
    });
  }
}
