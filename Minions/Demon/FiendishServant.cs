// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.FiendishServant
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class FiendishServant(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "YOD_026";
  public const string Text = "<b>Deathrattle:</b> Give this minion's Attack to a random friendly minion.";
  public const string GoldenText = "<b>Deathrattle:</b> Give this minion's Attack to a random friendly minion, twice.";

  public Action<Minion> GetDeathrattle() => FiendishServant.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion1;
        if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
          minion1.IncreaseStats(minion.attack(), 0);
      }
    });
  }
}
