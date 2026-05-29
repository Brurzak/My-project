// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.Voidlord
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class Voidlord(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_LOOT_368";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Summon three 1/3 Demons with <b>Taunt</b>.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Summon three 2/6 Demons with <b>Taunt</b>.";

  public Action<Minion> GetDeathrattle() => Voidlord.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("CS2_065", golden), new Summon("CS2_065", golden), new Summon("CS2_065", golden)));
  }
}
