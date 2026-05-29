// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.LightfeatherScreecher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class LightfeatherScreecher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG33_841";
  public const string Text = "<b>Taunt</b> <b>Start of Combat:</b> Give your left-most Beast <b>Windfury</b> and <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Taunt</b> <b>Start of Combat:</b> Give your 2 left-most Beasts <b>Windfury</b> and <b>Divine Shield</b>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion minion in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsBeast() && x.IsAlive())).Take<Minion>(this.DoubleIfGolden(1)).ToList<Minion>())
      {
        minion.div = 1;
        minion.windfury = true;
      }
    });
  }
}
