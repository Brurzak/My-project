// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.SilithidBurrower
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class SilithidBurrower(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity,
  IOnMinionRebornResetCounters,
  IAvenge
{
  public const string CardId = "BG29_871";
  public const string Text = "<b>Deathrattle:</b> Give your Beasts +{0}/+{0}. <b>Avenge (1):</b> Improve this by +1/+1 permanently.";
  public const string GoldenText = "<b>Deathrattle:</b> Give your Beasts +{0}/+{0}. <b>Avenge (1):</b> Improve this by +2/+2 permanently.";
  private int _avengeTriggers;

  public Action<Minion> GetDeathrattle() => this.Deathrattle();

  public Action<Minion> Deathrattle()
  {
    return (Action<Minion>) (minion =>
    {
      int startBuff = this.StartBuff;
      if (minion is SilithidBurrower)
        startBuff += this.InCombatIncrease;
      foreach (Minion minion1 in minion.FriendlySide)
      {
        if (minion1.IsBeast())
          minion1.IncreaseStats(startBuff);
      }
    });
  }

  public int StartBuff => this.ScriptDataNum1;

  public int InCombatIncrease => this.DoubleIfGolden(this._avengeTriggers);

  public int AvengeRequirement => 1;

  public Action? OnMinionRebornResetCounters()
  {
    return (Action) (() =>
    {
      this.ScriptDataNum1 = this.DoubleIfGolden(1);
      this._avengeTriggers = 0;
    });
  }

  public Action? OnAvenge() => (Action) (() => ++this._avengeTriggers);
}
