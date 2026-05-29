// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.GemSmuggler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class GemSmuggler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_155";
  public const string Text = "<b>Battlecry:</b> This plays 2 <b>Blood Gems</b> on all your other minions.";
  public const string GoldenText = "<b>Battlecry:</b> This plays 4 <b>Blood Gems</b> on all your other minions.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(2);
      foreach (Minion target in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this && x.IsAlive())))
      {
        for (int index = 0; index < num; ++index)
          this.Simulator.CastBloodGem(target, (Entity) this);
      }
    });
  }
}
