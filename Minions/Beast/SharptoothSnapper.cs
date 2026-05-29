// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SharptoothSnapper
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SharptoothSnapper(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity,
  IOnAfterAttackStep
{
  public const string CardId = "BG32_201";
  public const string Text = "When you have space in combat, summon a 3/1 Beast. It attacks immediately.";
  public const string GoldenText = "When you have space in combat, summon two 3/1 Beasts. They attack immediately.";
  private int _summoned;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat() => new Action(this.CheckForSummon);

  public void OnAfterAttackStep() => this.CheckForSummon();

  private void CheckForSummon()
  {
    int num = this.golden ? 2 : 1;
    if (this.FriendlySide.Count == 7 || this._summoned == num)
      return;
    ++this._summoned;
    this.TrySummonMinion(new Summon("BG32_201t"));
  }
}
