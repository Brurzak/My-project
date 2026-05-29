// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.LostSpirit
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class LostSpirit(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_GIL_513";
  public const string Text = "<b>Deathrattle:</b> Give your minions +1 Attack.";

  public Action<Minion> GetDeathrattle() => LostSpirit.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())))
        minion1.IncreaseStats(1, 0);
    });
  }
}
