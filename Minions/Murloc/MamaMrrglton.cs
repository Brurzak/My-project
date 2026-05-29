// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.MamaMrrglton
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class MamaMrrglton(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG35_140";
  public const string Text = "<b>Battlecry:</b> Give your other Murlocs +{0} Attack. <i>(Improved by each Mrrglton you played this game!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give your other Murlocs +{0} Attack. <i>(Improved by each Mrrglton you played this game!)</i>";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsMurloc() && x.IsAlive())).ToList<Minion>();
      int scriptDataNum1 = this.ScriptDataNum1;
      foreach (Minion minion in list)
        minion.IncreaseStats(scriptDataNum1, 0);
    });
  }
}
