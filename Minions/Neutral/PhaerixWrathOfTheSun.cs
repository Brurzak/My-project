// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.PhaerixWrathOfTheSun
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class PhaerixWrathOfTheSun(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG28_403";
  public const string Text = "<b><b>Divine Shield</b> Avenge (4):</b> Give a random friendly minion <b><b>Divine Shield</b>.</b>";
  public const string GoldenText = "<b><b>Divine Shield</b> Avenge (4):</b> Give two random friendly minions <b><b>Divine Shield</b>.</b>";

  public int AvengeRequirement => 4;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      Minion minion;
      for (int index = 0; index < num && this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && !x.hasDiv)).ToList<Minion>().TryGetRandom<Minion>(out minion); ++index)
        minion.div = 1;
    });
  }
}
