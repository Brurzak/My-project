// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.TimewarpedCollector
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class TimewarpedCollector(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_Giant_680";
  public const string Text = "Also damages adjacent minions. <b>Rally:</b> If you control 4 Golden minions, gain <b>Divine Shield</b>.";
  public const string GoldenText = "Also damages adjacent minions. <b>Rally:</b> If you control 4 Golden minions, gain <b>Divine Shield</b>.";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.golden && x.IsAlive())).ToList<Minion>().Count<Minion>() < 4)
        return;
      this.div = 1;
    });
  }
}
