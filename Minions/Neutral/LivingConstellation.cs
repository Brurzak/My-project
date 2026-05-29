// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.LivingConstellation
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class LivingConstellation(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG27_001";
  public const string Text = "<b>Battlecry:</b> Give a minion +1/+1 for each type you control.";
  public const string GoldenText = "<b>Battlecry:</b> Give a minion +2/+2 for each type you control.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(this.FriendlySide.GetRandomPerRace().Count<Minion>());
      Minion minion;
      if (!this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion))
        return;
      minion.IncreaseStats(num, num);
    });
  }
}
