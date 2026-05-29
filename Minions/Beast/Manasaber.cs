// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.Manasaber
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class Manasaber(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_800";
  public const string Text = "<b>Deathrattle:</b> Summon two 0/1 Cublings with <b>Taunt</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two 0/2 Cublings with <b>Taunt</b>.";
  public const string SummonCardId = "BG26_800t";

  public Action<Minion> GetDeathrattle() => Manasaber.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("BG26_800t", golden), new Summon("BG26_800t", golden)));
  }
}
