// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.Smogger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class Smogger(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG21_021";
  public const string Text = "<b>Battlecry:</b> Give a friendly Elemental stats equal to your Tier.";
  public const string GoldenText = "<b>Battlecry:</b> Give a friendly Elemental stats equal to your Tier twice.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsElemental() && x.IsAlive())).ToList<Minion>();
      int num1 = this.ControlledByPlayer ? this.Simulator.PlayerInput.Tier : this.Simulator.OpponentInput.Tier;
      int num2 = this.golden ? 2 : 1;
      Minion minion;
      ref Minion local = ref minion;
      if (!list.TryGetRandom<Minion>(out local))
        return;
      for (int index = 0; index < num2; ++index)
        minion.IncreaseStats(num1, num1, (Entity) this);
    });
  }
}
