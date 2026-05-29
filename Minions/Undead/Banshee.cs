// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.Banshee
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class Banshee(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG_RLK_957";
  public const string Text = "<b>Deathrattle:</b> Give a random friendly Undead +2/+1.";

  public Action<Minion> GetDeathrattle() => Banshee.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion minion1;
      if (!minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsUndead() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
        return;
      minion1.IncreaseStats(2, 1);
    });
  }
}
