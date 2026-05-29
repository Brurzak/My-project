// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.EternalSummoner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class EternalSummoner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_009";
  public const string Text = "<b><b>Reborn</b>.</b> <b>Deathrattle:</b> Summon 1 Eternal Knight.";
  public const string GoldenText = "<b>Reborn</b>. <b>Deathrattle:</b> Summon a Golden Eternal Knight.";

  public Action<Minion> GetDeathrattle() => EternalSummoner.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinion(Summon.FromCardId("BG25_008", golden)));
  }
}
