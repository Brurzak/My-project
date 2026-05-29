// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.FoulEgg
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class FoulEgg(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_RLK_833";
  public const string Text = "<b>Deathrattle:</b> Summon a 3/3 Undead Chicken.";

  public Action<Minion> GetDeathrattle() => FoulEgg.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion((Summon) "RLK_833t"));
  }
}
