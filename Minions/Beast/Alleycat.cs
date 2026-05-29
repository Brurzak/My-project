// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.Alleycat
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class Alleycat(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG_CFM_315";
  public const string Text = "<b>Battlecry:</b> Summon a 1/1 Cat.";
  public const string GoldenText = "<b>Battlecry:</b> Summon a 2/2 Cat.";

  public Action? OnBattlecry()
  {
    return (Action) (() => this.TrySummonMinion(new Summon("BG_CFM_315t", this.golden)));
  }
}
