// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.CaptainSanders
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class CaptainSanders(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG25_034";
  public const string Text = "<b>Battlecry:</b> Make a friendly minion from Tier 6 or below Golden.";
  public const string GoldenText = "<b>Battlecry:</b> Make two friendly minions from Tier 6 or below Golden.";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.DoubleIfGolden(1);
      for (int index = 0; index < num; ++index)
      {
        Minion minion;
        if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.CanBeMadeGolden() && x.tier <= 6)).ToList<Minion>().TryGetRandom<Minion>(out minion))
          minion.golden = true;
      }
    });
  }
}
