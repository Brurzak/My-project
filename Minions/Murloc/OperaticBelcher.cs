// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Murloc.OperaticBelcher
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Murloc;

public class OperaticBelcher(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG26_888";
  public const string Text = "<b>Venomous.</b> <b>Deathrattle:</b> Give a friendly Murloc <b>Venomous</b>.";
  public const string GoldenText = "<b>Venomous.</b> <b>Deathrattle:</b> Give 2 friendly Murlocs <b>Venomous</b>.";

  public Action<Minion> GetDeathrattle() => OperaticBelcher.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        Minion minion1;
        if (minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => !x.venomous && x.IsMurloc())).ToList<Minion>().TryGetRandom<Minion>(out minion1))
          minion1.venomous = true;
      }
    });
  }
}
