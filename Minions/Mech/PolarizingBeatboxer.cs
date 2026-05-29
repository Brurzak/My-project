// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.PolarizingBeatboxer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using HearthDb;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class PolarizingBeatboxer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMagnetized,
  IEntity
{
  public const string CardId = "BG26_149";
  public const string Text = "Whenever you <b>Magnetize</b> to a different minion, it also <b>Magnetizes</b> to this.";
  public const string GoldenText = "Whenever you <b>Magnetize</b> to a different minion, it also <b>Magnetizes</b> to this twice.";

  public Action? OnFriendlyMagnetized(
    Minion friendly,
    Card magnetic,
    Entity source,
    int extraAttack,
    int extraHealth)
  {
    return (Action) (() =>
    {
      if (friendly.CardID == "BG26_149")
        return;
      this.Simulator.MagnetizeMech((Minion) this, magnetic.Id, (Entity) this, extraAttack, extraHealth);
      if (!this.golden)
        return;
      this.Simulator.MagnetizeMech((Minion) this, magnetic.Id, (Entity) this, extraAttack, extraHealth);
    });
  }
}
