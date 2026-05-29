// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.EliteNavigator
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class EliteNavigator(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG32_231";
  public const string Text = "<b>Battlecry:</b> Make a friendly Pirate from Tier 4 or below Golden.";
  public const string GoldenText = "<b>Battlecry:</b> Make 2 friendly Pirates from Tier 4 or below Golden.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsPirate() && x != null && !x.golden && x.tier <= 4)).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      int num = this.DoubleIfGolden(1);
      Minion minion;
      for (int index = 0; index < num && list.TryGetRandom<Minion>(out minion); ++index)
      {
        minion.TryMakeGolden(true, (Entity) this);
        list.Remove(minion);
      }
    });
  }
}
