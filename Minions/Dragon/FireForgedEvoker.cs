// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.FireForgedEvoker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class FireForgedEvoker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG32_822";
  public const string Text = "<b>Start of Combat:</b> Give your Dragons +{0}/+{1}. Improves permanently after you cast a Tavern spell.";
  public const string GoldenText = "<b>Start of Combat:</b> Give your Dragons +{0}/+{1}. Improves permanently after you cast a Tavern spell.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int scriptDataNum1 = this.ScriptDataNum1;
      int scriptDataNum2 = this.ScriptDataNum2;
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsDragon())))
        minion.IncreaseStats(scriptDataNum1, scriptDataNum2);
    });
  }
}
