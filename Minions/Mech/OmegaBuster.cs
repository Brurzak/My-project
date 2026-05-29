// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Mech.OmegaBuster
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Linq;

#nullable enable
namespace BobsBuddy.Minions.Mech;

public class OmegaBuster(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG21_025";
  public const string Text = "<b>Deathrattle:</b> Summon six 1/1 Microbots. For each that doesn't fit, give your Mechs +1/+1.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon six 2/2 Microbots. For each that doesn't fit, give your Mechs +2/+2.";
  private const int SummonCount = 6;

  public Action<Minion> GetDeathrattle() => OmegaBuster.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num1 = golden ? 2 : 1;
      int num2 = 6 - minion.TrySummonMinions(Enumerable.Range(1, 6).Select<int, Summon>((Func<int, Summon>) (x => new Summon("BG_BOT_312t", golden))).ToList<Summon>()).Count;
      foreach (Minion minion1 in minion.FriendlySide.Where<Minion>((Func<Minion, bool>) (x => x.IsAlive() && x.IsMech())))
        minion1.IncreaseStats(num2 * num1);
    });
  }
}
