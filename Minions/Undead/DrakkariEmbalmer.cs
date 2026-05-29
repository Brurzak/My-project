// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.DrakkariEmbalmer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class DrakkariEmbalmer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG26_RLK_119";
  public const string Text = "<b>Battlecry:</b> Give a friendly Undead <b>Reborn</b>.";
  public const string GoldenText = "<b>Battlecry:</b> Give 2 friendly Undead <b>Reborn</b>.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsUndead() && x != null && !x.reborn)).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      int num = this.DoubleIfGolden(1);
      Minion minion;
      for (int index = 0; index < num && list.TryGetRandom<Minion>(out minion); ++index)
      {
        minion.reborn = true;
        list.Remove(minion);
      }
    });
  }
}
