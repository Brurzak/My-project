// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Naga.SunkenAdvocate
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Naga;

public class SunkenAdvocate(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG34_930";
  public const string Text = "<b>Rally:</b> Give your other Naga +{0} Attack permanently. <i>(Improved by each spell you cast this turn!)</i>";
  public const string GoldenText = "<b>Rally:</b> Give your other Naga +{0} Attack permanently. <i>(Improved by each spell you cast this turn!)</i>";

  public Action<Minion>? OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int attackBuff = this.DoubleIfGolden(this.ScriptDataNum1);
      foreach (Minion minion1 in this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsNaga() && x != this && x.IsAlive())).ToList<Minion>())
        minion1.IncreaseStats(attackBuff, 0);
    });
  }
}
