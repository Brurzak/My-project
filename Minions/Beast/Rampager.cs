// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.Rampager
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class Rampager(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IOnRally,
  IEntity
{
  public const string CardId = "BG29_809";
  public const string Text = "<b>Rally:</b> Deal 1 damage to your other minions.";
  public const string GoldenText = "<b>Rally:</b> Deal 1 damage to your other minions twice.";

  public Action<Minion> OnRally(bool isGolden, Minion target)
  {
    return (Action<Minion>) (minion =>
    {
      int num = this.golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
        this.Simulator.ProcessDamage(this.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != this)).Select<Minion, Damage>((Func<Minion, Damage>) (x => new Damage(1, x, (Entity) this))));
    });
  }
}
