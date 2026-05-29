// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.ExceptionalCaretaker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class ExceptionalCaretaker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity,
  IDeathrattle,
  IOnRally
{
  public const string CardId = "BG33_701";
  public const string Text = "<b><b>Battlecry</b>, <b>Deathrattle</b>, and <b>Rally:</b></b> Give your other minions +{0}/+{1}.";
  public const string GoldenText = "<b><b>Battlecry</b>, <b>Deathrattle</b>, and <b>Rally:</b></b> Give your other minions +{0}/+{1}.";

  public Action<Minion> GetDeathrattle() => ExceptionalCaretaker.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != minion && x.IsAlive())).ToList<Minion>();
      int by = golden ? 4 : 2;
      foreach (Minion minion1 in list)
        minion1.IncreaseStats(by);
    });
  }

  public Action? OnBattlecry() => (Action) (() => this.GetDeathrattle()((Minion) this));

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      if (!(minion is ExceptionalCaretaker exceptionalCaretaker2))
        return;
      exceptionalCaretaker2.GetDeathrattle()(minion);
    });
  }
}
