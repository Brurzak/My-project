// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.RatPack
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class RatPack(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_CFM_316";
  public const string Text = "<b>Deathrattle:</b> Summon a number of 1/1 Rats equal to this minion's Attack.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a number of 2/2 Rats equal to this minion's Attack.";
  public const string SummonCardId = "BG_CFM_316t";

  public Action<Minion> GetDeathrattle() => RatPack.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Summon> list = Enumerable.Range(1, minion.attack()).Select<int, Summon>((Func<int, Summon>) (x => new Summon("BG_CFM_316t", golden))).ToList<Summon>();
      minion.TrySummonMinions(list);
    });
  }
}
