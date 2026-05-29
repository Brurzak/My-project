// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.ChargingCzarina
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class ChargingCzarina(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnAfterTavernSpellCast,
  IEntity
{
  public const string CardId = "BG28_741";
  public const string Text = "<b>Divine Shield</b> Whenever you cast a Tavern spell, give your minions with <b>Divine Shield</b> +{0} Attack.";
  public const string GoldenText = "<b>Divine Shield</b> Whenever you cast a Tavern spell, give your minions with <b>Divine Shield</b> +{0} Attack.";

  public Action? OnAfterTavernSpellCast(Minion? target)
  {
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(3);
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.hasDiv)).ToList<Minion>())
        minion.IncreaseStats(attackBuff, 0, (Entity) this);
    });
  }
}
