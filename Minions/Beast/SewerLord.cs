// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SewerLord
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SewerLord(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG35_604";
  public const string Text = "<b>Deathrattle:</b> Summon two Sewer Rats that summon 2/3 Turtles with <b>Taunt</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two Golden Sewer Rats that summon 4/6 Turtles with <b>Taunt</b>.";

  public Action<Minion> GetDeathrattle() => SewerLord.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("BG19_010", golden), new Summon("BG19_010", golden)));
  }
}
