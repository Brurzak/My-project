// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.CorruptedBristler
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class CorruptedBristler(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG32_431";
  public const string Text = "<b>Deathrattle:</b> Summon a Golem with stats equal to this minion's <b>Blood Gems</b>.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon 2 Golems with stats equal to this minion's <b>Blood Gems</b>.";

  public Action<Minion> GetDeathrattle() => CorruptedBristler.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      (int num3, int num4) = minion.StatsFromBloodGems;
      if (num4 <= 0)
        return;
      minion.TrySummonMinion(new Summon("BG30_MagicItem_442t")
      {
        SetStats = new (int, int)?((num3, num4))
      });
      if (!golden)
        return;
      minion.TrySummonMinion(new Summon("BG30_MagicItem_442t")
      {
        SetStats = new (int, int)?((num3, num4))
      });
    });
  }
}
