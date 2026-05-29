// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.HogWatcher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class HogWatcher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG33_888";
  public const string Text = "<b>Battlecry:</b> Get a <b>Blood Gem</b> that also gives a Quilboar <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Battlecry:</b> Get 2 <b>Blood Gems</b> that also give a Quilboar <b>Divine Shield</b>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
        this.AddBloodGemToFriendlyHand();
    });
  }
}
