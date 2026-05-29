// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.Imprisoner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class Imprisoner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BGS_014";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Summon a 1/1 Imp.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Summon a 2/2 Imp.";

  public Action<Minion> GetDeathrattle() => Imprisoner.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(new Summon("BG_BRM_006t", golden)));
  }
}
