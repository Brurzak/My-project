// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.BriarbackDrummer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class BriarbackDrummer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_683";
  public const string Text = "<b>Battlecry:</b> Get a Blood Gem Barrage.";
  public const string GoldenText = "<b>Battlecry:</b> Get 2 Blood Gem Barrages.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      this.AddBloodGemToFriendlyHand();
      if (!this.golden)
        return;
      this.AddBloodGemToFriendlyHand();
    });
  }
}
