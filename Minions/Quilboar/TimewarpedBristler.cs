// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedBristler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using BobsBuddy.Utils;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedBristler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_104";
  public const string Text = "<b>Deathrattle:</b> Give this minion's <b>Blood Gems</b> to {0} different friendly Quilboar.";
  public const string GoldenText = "<b>Deathrattle:</b> Give this minion's <b>Blood Gems</b> to {0} different friendly Quilboar, twice.";

  public Action<Minion> GetDeathrattle() => TimewarpedBristler.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      (int attackBuff2, int healthBuff2) = minion.StatsFromBloodGems;
      int num = golden ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        foreach (Minion randomElement in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsQuilboar() && x != minion)).ToList<Minion>().GetRandomElements<Minion>(2))
          randomElement.IncreaseStats(attackBuff2, healthBuff2);
      }
    });
  }
}
