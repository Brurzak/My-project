// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.ScarletSkull
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class ScarletSkull(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_022";
  public const string Text = "<b>Reborn</b> <b>Deathrattle:</b> Give a friendly Undead +1/+2.";
  public const string GoldenText = "<b>Reborn</b> <b>Deathrattle:</b> Give a friendly Undead +2/+4.";

  public Action<Minion> GetDeathrattle() => ScarletSkull.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion minion1;
      if (!minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsUndead())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
        return;
      minion1.IncreaseStats(golden ? 2 : 1, golden ? 4 : 2);
    });
  }
}
