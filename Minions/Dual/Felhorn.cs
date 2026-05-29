// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dual.Felhorn
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dual;

public class Felhorn(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG34_781";
  public const string Text = "<b>Battlecry:</b> Give your other Demons and Beasts +{0}/+{1} and deal 1 damage to them, twice.";
  public const string GoldenText = "<b>Battlecry:</b> Give your other Demons and Beasts +{0}/+{1} and deal 1 damage to them, four times.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x =>
      {
        if (x == this || !x.IsAlive())
          return false;
        return x.IsDemon() || x.IsBeast();
      })).ToList<Minion>();
      int num = this.golden ? 4 : 2;
      for (int index = 0; index < num; ++index)
      {
        foreach (Minion target in list)
        {
          target.IncreaseStats(1, 2);
          this.Simulator.ProcessDamage(1, target, (Entity) this);
        }
      }
    });
  }
}
