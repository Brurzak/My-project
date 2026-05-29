// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.ReefExplorer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class ReefExplorer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG23_016";
  public const string Text = "<b><b>Battlecry:</b> Discover</b> a minion of a type you don't control.";
  public const string GoldenText = "<b>Battlecry: Discover</b> 2 minions of a type you don't control.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.AddMinionToFriendlyHand();
      if (!this.golden)
        return;
      this.AddMinionToFriendlyHand();
    });
  }
}
