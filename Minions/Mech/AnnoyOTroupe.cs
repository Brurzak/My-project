// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.AnnoyOTroupe
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class AnnoyOTroupe(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_ETC_321";
  public const string Text = "<b>Taunt</b>, <b>Divine Shield</b> <b>Deathrattle:</b> Summon three 1/2 Mechs with <b>Taunt</b> and <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Taunt</b>, <b>Divine Shield</b> <b>Deathrattle:</b> Summon three 2/4 Mechs with <b>Taunt</b> and <b>Divine Shield</b>.";

  public Action<Minion> GetDeathrattle() => AnnoyOTroupe.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.TrySummonMinions(new Summon("BG_GVG_085", golden), new Summon("BG_GVG_085", golden), new Summon("BG_GVG_085", golden)));
  }
}
