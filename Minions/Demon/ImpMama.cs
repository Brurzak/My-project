// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.ImpMama
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using HearthDb.Enums;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class ImpMama(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnTakeDamage,
  IEntity
{
  public const string CardId = "BGS_044";
  public const string Text = "Whenever this minion takes damage, summon a random Demon and give it <b>Taunt</b>.";
  public const string GoldenText = "Whenever this minion takes damage, summon 2 random Demons and give them <b>Taunt</b>.";

  public Action? OnTakeDamage(int value)
  {
    return (Action) (() =>
    {
      foreach (Minion summonRandomMinion in this.TrySummonRandomMinions(this.Simulator.MinionFactory.MinionPoolOptionsPlayerAndRace(this.ControlledByPlayer, (Race) 15).Where<Card>((Func<Card, bool>) (x => x.Id != "BGS_044")).Select<Card, Summon>(new Func<Card, Summon>(Summon.FromCard)).ToList<Summon>(), this.DoubleIfGolden(1)))
        summonRandomMinion.taunt = true;
    });
  }
}
