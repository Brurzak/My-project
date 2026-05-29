// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Neutral.NadinatheRed
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Neutral;

public class NadinatheRed(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BGS_040";
  public const string Text = "<b>Deathrattle:</b> Give 3 friendly Dragons <b>Divine Shield</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Give 6 friendly Dragons <b>Divine Shield</b>.";

  public Action<Minion> GetDeathrattle() => NadinatheRed.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Minion> list = minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsDragon() && x.IsAlive() && !x.hasDiv)).ToList<Minion>();
      Minion minion1;
      for (int index = 0; index < (golden ? 6 : 3) && list.TryGetRandom<Minion>(out minion1); ++index)
      {
        minion1.div = 1;
        list.Remove(minion1);
      }
    });
  }
}
