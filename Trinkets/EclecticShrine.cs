// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.EclecticShrine
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;

#nullable enable
namespace BobsBuddy.Trinkets;

public class EclecticShrine(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG32_MagicItem_280";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int attackBuff = Math.Max(this.ScriptDataNum1, 3);
      int healthBuff = Math.Max(this.ScriptDataNum2, 2);
      foreach (Minion minion in this.FriendlySide.GetRandomPerRace())
        minion.IncreaseStats(attackBuff, healthBuff);
      ++this.ScriptDataNum1;
      ++this.ScriptDataNum2;
    });
  }
}
