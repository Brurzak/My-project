// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.BubbleGunner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class BubbleGunner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG31_149";
  public const string Text = "<b>Battlecry:</b> Gain a random <b>Bonus Keyword</b>.";
  public const string GoldenText = "<b>Battlecry:</b> Gain 2 random <b>Bonus Keywords</b>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.AddRandomBonusKeyword();
      if (!this.golden)
        return;
      this.AddRandomBonusKeyword();
    });
  }
}
