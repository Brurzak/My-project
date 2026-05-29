// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.AnnihilanBattlemaster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class AnnihilanBattlemaster(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGS_010";
  public const string Text = "<b>Battlecry:</b> Gain +2 Health. <i>(Improved by each Health your hero is missing!)</i>";

  public Action? OnBattlecry()
  {
    int friendlyDamage = this.ControlledByPlayer ? this.Simulator.PlayerInput.DamageTaken : this.Simulator.OpponentInput.DamageTaken;
    return friendlyDamage == 0 ? (Action) null : (Action) (() => this.IncreaseStats(0, this.DoubleIfGolden(2 * friendlyDamage)));
  }
}
