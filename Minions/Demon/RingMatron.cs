// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.RingMatron
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class RingMatron(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_DMF_533";
  public const string Text = "<b>Taunt</b> <b>Deathrattle:</b> Summon two 3/2 Imps.";
  public const string GoldenText = "<b>Taunt</b> <b>Deathrattle:</b> Summon two 6/4 Imps.";

  public Action<Minion> GetDeathrattle() => RingMatron.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("BG_DMF_533t", golden), new Summon("BG_DMF_533t", golden)));
  }
}
