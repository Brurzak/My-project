// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.TimewarpedLeapfrogger
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class TimewarpedLeapfrogger(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_031";
  public const string Text = "<b>Taunt</b>, <b>Reborn</b> <b>Deathrattle:</b> Give a friendly Beast +1/+1 and this <b>Deathrattle</b>.";
  public const string GoldenText = "<b>Taunt</b>, <b>Reborn</b> <b>Deathrattle:</b> Give a friendly Beast +2/+2 and this <b>Deathrattle</b>.";

  public Action<Minion> GetDeathrattle() => TimewarpedLeapfrogger.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      Minion minion1;
      if (!minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsBeast() && x.IsAlive())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
        return;
      minion1.IncreaseStats(golden ? 2 : 1);
      minion1.AdditionalDeathrattles.Add(TimewarpedLeapfrogger.Deathrattle(golden));
    });
  }
}
