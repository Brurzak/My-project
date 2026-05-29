// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.BurgeoningWhelp
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class BurgeoningWhelp(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IBattlecry
{
  public const string CardId = "BG34_402";
  public const string Text = "<b>Battlecry and Deathrattle:</b> Your Whelps have +{0}/+{1} this game.";
  public const string GoldenText = "<b>Battlecry and Deathrattle:</b> Your Whelps have +{0}/+{1} this game.";

  public Action<Minion> GetDeathrattle() => BurgeoningWhelp.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = golden ? 6 : 3;
      int healthBuff = golden ? 6 : 3;
      globalModifier.IncreaseWhelpBonus(atkBuff, healthBuff, (Entity) minion);
    });
  }

  public Action? OnBattlecry()
  {
    return (Action) (() => BurgeoningWhelp.Deathrattle(this.golden)((Minion) this));
  }
}
