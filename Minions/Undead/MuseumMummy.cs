// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.MuseumMummy
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class MuseumMummy(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG30_850";
  public const string Text = "<b>Battlecry:</b> Summon a 1/1 Skeleton.";
  public const string GoldenText = "<b>Battlecry:</b> Summon a 2/2 Skeleton.";
  public const string SummonCardId = "BG_ICC_026t";

  public Action? OnBattlecry()
  {
    return (Action) (() => this.TrySummonMinion(new Summon("BG_ICC_026t", this.golden)));
  }
}
