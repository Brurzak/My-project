// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Demon.IckyImp
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Demon;

public class IckyImp(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG21_029";
  public const string Text = "<b>Deathrattle:</b> Summon two 1/1 Imps.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon four 1/1 Imps.";

  public Action<Minion> GetDeathrattle() => IckyImp.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      minion.TrySummonMinions(new Summon("BG_BRM_006t"), new Summon("BG_BRM_006t"));
      if (!golden)
        return;
      minion.TrySummonMinions(new Summon("BG_BRM_006t"), new Summon("BG_BRM_006t"));
    });
  }
}
