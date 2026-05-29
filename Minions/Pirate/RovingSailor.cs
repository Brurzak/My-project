// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.RovingSailor
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class RovingSailor(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG35_702";
  public const string Text = "<b>Battlecry:</b> Give a friendly minion +{0}/+{1}. <i>(Improved by each Tavern spell you cast this turn!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give a friendly minion +{0}/+{1} twice. <i>(Improved by each Tavern spell you cast this turn!)</i>";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion;
        if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion))
          minion.IncreaseStats(this.ScriptDataNum1, this.ScriptDataNum2, (Entity) this);
      }
    });
  }
}
