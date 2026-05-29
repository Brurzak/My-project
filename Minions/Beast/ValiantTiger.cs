// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.ValiantTiger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class ValiantTiger(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_202";
  public const string Text = "<b><b>Taunt</b>. Deathrattle:</b> Give your right-most Beast +5/+5 permanently.";
  public const string GoldenText = "<b><b>Taunt</b>. Deathrattle:</b> Give your right-most Beast +10/+10 permanently.";

  public Action<Minion> GetDeathrattle() => ValiantTiger.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsBeast())).ToList<Minion>();
      if (!list.Any<Minion>())
        return;
      list.LastOrDefault<Minion>()?.IncreaseStats(golden ? 10 : 5);
    });
  }
}
