// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.InfestedWolf
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class InfestedWolf(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "OG_216";
  public const string Text = "<b>Deathrattle:</b> Summon two 1/1 Spiders.";

  public Action<Minion> GetDeathrattle() => InfestedWolf.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("OG_216a", golden), new Summon("OG_216a", golden)));
  }
}
