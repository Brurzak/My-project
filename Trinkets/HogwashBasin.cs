// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Trinkets.HogwashBasin
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Trinkets;

public class HogwashBasin(string cardId, Simulator simulator, bool controlledByPlayer) : 
  Trinket(cardId, simulator, controlledByPlayer),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG32_MagicItem_904";
  private const int BloodGemsCast = 3;

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      foreach (Minion target in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>())
      {
        for (int index = 0; index < 3; ++index)
          this.Simulator.CastBloodGem(target, (Entity) this);
      }
    });
  }
}
