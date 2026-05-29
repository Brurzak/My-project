// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.TimewarpedIckyImp
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class TimewarpedIckyImp(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_674";
  public const string Text = "<b>Deathrattle:</b> Summon 2 Imps with this minion's maximum stats.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon 4 Imps with this minion's maximum stats.";

  public Action<Minion> GetDeathrattle() => TimewarpedIckyImp.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      (int, int) valueTuple = (minion.maxAttack, minion.maxHealth);
      Summon summon1 = new Summon("BG_BRM_006t");
      Summon summon2 = new Summon("BG_BRM_006t");
      summon1.SetStats = new (int, int)?(valueTuple);
      summon2.SetStats = new (int, int)?(valueTuple);
      minion.TrySummonMinions(summon1, summon2);
      if (!golden)
        return;
      Summon summon3 = new Summon("BG_BRM_006t");
      Summon summon4 = new Summon("BG_BRM_006t");
      summon3.SetStats = new (int, int)?(valueTuple);
      summon4.SetStats = new (int, int)?(valueTuple);
      minion.TrySummonMinions(summon3, summon4);
    });
  }
}
