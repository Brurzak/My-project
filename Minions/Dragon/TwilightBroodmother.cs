// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.TwilightBroodmother
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class TwilightBroodmother(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_731";
  public const string Text = "<b>Deathrattle:</b> Summon 2 Twilight Hatchlings. Give them <b>Taunt</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon 4 Twilight Hatchlings. Give them <b>Taunt</b>.";
  public const string SummonCardId = "BG34_630";

  public Action<Minion> GetDeathrattle() => TwilightBroodmother.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      for (int index = 0; index < (golden ? 2 : 1); ++index)
        minion.TrySummonMinions(new Summon("BG34_630"), new Summon("BG34_630")).ForEach((Action<Minion>) (x => x.taunt = true));
    });
  }
}
