// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.LovesickBalladist
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Trinkets;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class LovesickBalladist(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IBattlecry,
  IEntity
{
  public const string CardId = "BG26_814";
  public const string Text = "<b>Battlecry:</b> Give a Pirate +{1} Health. <i>(Improved by each Gold you spent this turn!)</i>[x]<b>Battlecry:</b> Give a Pirate +{0}/+{1}. <i>(Improved by each Gold you spent this turn!)</i>";
  public const string GoldenText = "<b>Battlecry:</b> Give a Pirate +{1} Health twice. <i>(Improved by each Gold you spent this turn!)</i>[x]<b>Battlecry:</b> Give a Pirate +{0}/+{1} twice. <i>(Improved by each Gold you spent this turn!)</i>";

  public Action? OnBattlecry()
  {
    return (Action) (() =>
    {
      bool flag = this.FriendlyEntities.Any<Entity>((Func<Entity, bool>) (x => x is BalladistPortrait));
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion;
        if (this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsPirate())).Concat<Minion>(this.OpposingSide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsPirate()))).ToList<Minion>().TryGetRandom<Minion>(out minion))
          minion.IncreaseStats(flag ? this.ScriptDataNum1 : 0, this.ScriptDataNum2, (Entity) this);
      }
    });
  }
}
