// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Dragon.Stuntdrake
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Dragon;

public class Stuntdrake(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IAvenge,
  IEntity
{
  public const string CardId = "BG34_732";
  public const string Text = "<b>Avenge ({0}):</b> Give this minion's Attack to two different friendly minions.";
  public const string GoldenText = "<b>Avenge ({0}):</b> Give this minion's Attack to two different friendly minions, twice.";

  public int AvengeRequirement => 3;

  public Action? OnAvenge()
  {
    return (Action) (() =>
    {
      int num = this.golden ? 2 : 1;
      for (int index1 = 0; index1 < num; ++index1)
      {
        List<Minion> list = this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.CardID != "BG34_732")).ToList<Minion>();
        for (int index2 = 0; index2 < 2; ++index2)
        {
          Minion minion;
          if (list.TryGetRandom<Minion>(out minion))
          {
            minion.IncreaseStats(this.attack(), 0);
            list.Remove(minion);
          }
        }
      }
    });
  }
}
