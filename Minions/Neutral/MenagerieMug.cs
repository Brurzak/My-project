// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.MenagerieMug
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class MenagerieMug(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BGS_082";
  public const string Text = "<b>Battlecry:</b> Give 3 friendly minions of different types +1/+1.";
  public const string GoldenText = "<b>Battlecry:</b> Give 3 friendly minions of different types +2/+2.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(1);
      List<Minion> randomPerRace = this.FriendlySide.GetRandomPerRace();
      Minion minion;
      for (int index = 0; index < 3 && randomPerRace.TryGetRandom<Minion>(out minion); ++index)
      {
        minion.IncreaseStats(num, num);
        randomPerRace.Remove(minion);
      }
    });
  }
}
