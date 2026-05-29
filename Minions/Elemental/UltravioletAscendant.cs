// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Elemental.UltravioletAscendant
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Elemental;

public class UltravioletAscendant(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG31_810";
  public const string Text = "<b>Start of Combat:</b> Give your other Elementals +{0}/+{1}. <i>(Improves after you play an Elemental!)</i>";
  public const string GoldenText = "<b>Start of Combat:</b> Give your other Elementals +{0}/+{1}. <i>(Improves after you play an Elemental!)</i>";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int scriptDataNum1 = this.ScriptDataNum1;
      int scriptDataNum2 = this.ScriptDataNum2;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsElemental() && x != this)))
        minion.IncreaseStats(scriptDataNum1, scriptDataNum2, (Entity) this);
    });
  }
}
