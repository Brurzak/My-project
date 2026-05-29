// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.TimewarpedCaretaker
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;
using System.Collections.Generic;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class TimewarpedCaretaker(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_618";
  public const string Text = "<b>Deathrattle:</b> Summon five 1/1 Skeletons. Any that don't fit give your Undead +{0} Attack this game <i>(wherever they are)</i>.";
  public const string GoldenText = "<b>Deathrattle:</b> Summon ten 1/1 Skeletons. Any that don't fit give your Undead +{0} Attack this game <i>(wherever they are)</i>.";
  public const string SummonCardId = "BG_ICC_026t";

  public Action<Minion> GetDeathrattle() => TimewarpedCaretaker.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      List<Summon> summons = new List<Summon>()
      {
        new Summon("BG_ICC_026t"),
        new Summon("BG_ICC_026t"),
        new Summon("BG_ICC_026t"),
        new Summon("BG_ICC_026t"),
        new Summon("BG_ICC_026t")
      };
      if (golden)
        summons.AddRange((IEnumerable<Summon>) new List<Summon>()
        {
          new Summon("BG_ICC_026t"),
          new Summon("BG_ICC_026t"),
          new Summon("BG_ICC_026t"),
          new Summon("BG_ICC_026t"),
          new Summon("BG_ICC_026t")
        });
      List<Minion> minionList = minion.TrySummonMinions(summons);
      int num = summons.Count - minionList.Count;
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = num;
      globalModifier.IncreaseUndeadAttackBonus(atkBuff, (Entity) minion);
    });
  }
}
