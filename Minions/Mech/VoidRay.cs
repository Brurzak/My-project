// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.VoidRay
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class VoidRay(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyMinionIsAttacking,
  IEntity
{
  public const string CardId = "BG31_HERO_802pt5";
  public const string Text = "<b>Divine Shield</b>. Whenever a friendly minion attacks, give it and this minion +5 Attack permanently.";
  public const string GoldenText = "<b>Divine Shield</b>. Whenever a friendly minion attacks, give it and this minion +10 Attack permanently.";

  public Action? OnFriendlyMinionIsAttacking(Minion attacker, Minion target)
  {
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(5);
      if (attacker != this)
        this.IncreaseStats(attackBuff, 0);
      attacker.IncreaseStats(attackBuff, 0);
    });
  }
}
