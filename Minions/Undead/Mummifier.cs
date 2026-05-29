// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.Mummifier
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class Mummifier(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG28_309";
  public const string Text = "<b>Deathrattle:</b> Give a different friendly Undead <b>Reborn</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Give 2 different friendly Undead <b>Reborn</b>.";

  public Action<Minion> GetDeathrattle() => Mummifier.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      for (int index = 0; index < (golden ? 2 : 1); ++index)
      {
        Minion minion1;
        if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsUndead() && x.CardID != "BG28_309" && !x.reborn)).ToList<Minion>().TryGetRandom<Minion>(out minion1))
          minion1.reborn = true;
      }
    });
  }
}
