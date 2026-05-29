// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.MantidQueen
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class MantidQueen(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG22_402";
  public const string Text = "<b>Venomous</b>. <b>Start of Combat:</b> For each of your minion types gain +5/+5, <b>Windfury</b>, <b>Reborn</b>, or <b>Taunt</b>.";
  public const string GoldenText = "<b>Venomous</b>. <b>Start of Combat:</b> For each of your minion types gain +5/+5, <b>Windfury</b>, <b>Reborn</b>, or <b>Taunt</b>, twice.";
  private int _numBuffs;

  public void OnCombatStartSetup()
  {
    List<Minion> randomPerRace = this.FriendlySide.GetRandomPerRace();
    this._numBuffs = (this.golden ? 2 : 1) * randomPerRace.Count<Minion>();
  }

  public Action OnStartOfCombat()
  {
    return (Action) (() =>
    {
      List<Action> list = new List<Action>();
      for (int index = 0; index < this._numBuffs; ++index)
      {
        if (!this.windfury)
          list.Add(new Action(this.GainWindfury));
        if (!this.reborn)
          list.Add(new Action(this.GainReborn));
        if (!this.taunt)
          list.Add(new Action(this.GainTaunt));
        list.Add(new Action(this.GainStats));
        list.GetRandom<Action>()();
        list.Clear();
      }
    });
  }

  private void GainWindfury() => this.windfury = true;

  private void GainReborn() => this.reborn = true;

  private void GainTaunt() => this.taunt = true;

  private void GainStats() => this.IncreaseStats(5, 5);
}
