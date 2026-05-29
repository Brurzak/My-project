// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.KalecgosArcaneAspect
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class KalecgosArcaneAspect(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnFriendlyBattlecry,
  IEntity
{
  public const string CardId = "BGS_041";
  public const string Text = "After you trigger a <b>Battlecry</b>, give your Dragons +{0}/+{1}.";
  public const string GoldenText = "After you trigger a <b>Battlecry</b>, give your Dragons +{0}/+{1}.";

  public Action? OnFriendlyBattlecry()
  {
    return (Action) (() =>
    {
      int attackBuff = this.DoubleIfGolden(2);
      int healthBuff = this.DoubleIfGolden(2);
      foreach (Minion minion in this.FriendlySide)
      {
        if (minion.IsDragon() && minion.IsAlive())
          minion.IncreaseStats(attackBuff, healthBuff);
      }
    });
  }
}
