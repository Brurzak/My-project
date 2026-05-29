// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.WaywardGrimscale
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class WaywardGrimscale(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacked,
  IEntity
{
  public const string CardId = "BG28_406";
  public const string Text = "Whenever this is attacked, gain <b>Venomous</b>.";
  public const string GoldenText = "Whenever this is attacked, gain <b>Venomous</b>.";

  public Action? OnFriendlyMinionIsAttacked(Minion friendlyMinion, Minion attacker)
  {
    return (Action) (() =>
    {
      if (friendlyMinion != this)
        return;
      this.venomous = true;
    });
  }
}
