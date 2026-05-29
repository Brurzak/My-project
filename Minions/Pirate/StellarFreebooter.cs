// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.StellarFreebooter
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class StellarFreebooter(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG29_866";
  public const string Text = "<b>Taunt</b>. <b>Deathrattle:</b> Give another friendly Pirate Health equal to this minion's Attack.";
  public const string GoldenText = "<b>Taunt</b>. <b>Deathrattle:</b> Give another friendly Pirate Health equal to this minion's Attack, twice.";

  public Action<Minion> GetDeathrattle() => StellarFreebooter.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion minion1;
      if (!minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x != minion && x.IsPirate() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
        return;
      int healthBuff = minion.attack();
      minion1.IncreaseStats(0, healthBuff);
      if (!golden)
        return;
      minion1.IncreaseStats(0, healthBuff);
    });
  }
}
