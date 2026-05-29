// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.SunScreener
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class SunScreener(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnStartOfCombat,
  IEntity
{
  public const string CardId = "BG30_101";
  public const string Text = "<b>Start of Combat:</b> Give you and your opponent's 3 left- most minions <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Start of Combat:</b> Give you and your opponent's 6 left- most minions <b>Divine Shield</b>.";

  public void OnCombatStartSetup()
  {
  }

  public Action? OnStartOfCombat()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(3);
      List<Minion> list1 = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive())).ToList<Minion>();
      List<Minion> list2 = this.OpposingSide.Where<Minion>((Func<Minion, bool>) (m => m.IsAlive())).ToList<Minion>();
      for (int index = 0; index < list1.Count<Minion>(); ++index)
      {
        if (index < num)
          list1[index].div = 1;
      }
      for (int index = 0; index < list2.Count<Minion>(); ++index)
      {
        if (index < num)
          list2[index].div = 1;
      }
    });
  }
}
