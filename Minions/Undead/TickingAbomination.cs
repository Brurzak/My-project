// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TickingAbomination
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TickingAbomination(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_ICC_099";
  public const string Text = "<b>Deathrattle:</b> Deal 5 damage to your minions.";

  public Action<Minion> GetDeathrattle() => TickingAbomination.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion => minion.Simulator.ProcessDamage(minion.FriendlySide.Select<Minion, Damage>((Func<Minion, Damage>) (x => new Damage(5, x, (Entity) minion)))));
  }
}
