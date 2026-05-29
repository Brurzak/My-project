// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.VorkaiGuardian
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class VorkaiGuardian(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG32_867";
  public const string Text = "<b>Start of Combat:</b> If you have no minions with <b>Divine Shield</b>, give adjacent minions <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Start of Combat:</b> If you have no minions with <b>Divine Shield</b>, give adjacent minions <b>Divine Shield</b>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.hasDiv && x.IsAlive())).ToList<Minion>().Any<Minion>())
        return;
      foreach (Minion minion in new List<Minion>()
      {
        this.GetLeftNeighbor(),
        this.GetRightNeighbor()
      }.Where<Minion>((Func<Minion, bool>) (x => x != null && x.IsAlive())).Cast<Minion>().ToList<Minion>())
        minion.div = 1;
    });
  }
}
