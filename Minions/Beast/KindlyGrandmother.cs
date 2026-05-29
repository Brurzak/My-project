// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.KindlyGrandmother
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class KindlyGrandmother(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_KAR_005";
  public const string Text = "<b>Deathrattle:</b> Summon a 3/2 Big Bad Wolf.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon a 6/4 Big Bad Wolf.";

  public Action<Minion> GetDeathrattle() => KindlyGrandmother.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(new Summon("BG_KAR_005a", golden)));
  }
}
