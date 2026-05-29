// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SavannahHighmane
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SavannahHighmane(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_EX1_534";
  public const string Text = "<b>Deathrattle:</b> Summon two 2/2 Hyenas.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon two 4/4 Hyenas.";

  public Action<Minion> GetDeathrattle() => SavannahHighmane.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("BG_EX1_534t", golden), new Summon("BG_EX1_534t", golden)));
  }
}
