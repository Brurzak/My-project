// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.AnubarakNerubianKing
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class AnubarakNerubianKing(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG25_007";
  public const string Text = "<b>Deathrattle:</b> Your Undead have +1 Attack this game <i>(wherever they are)</i>.";
  public const string GoldenText = "<b>Deathrattle:</b> Your Undead have +2 Attack this game <i>(wherever they are)</i>.";

  public Action<Minion> GetDeathrattle() => AnubarakNerubianKing.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = golden ? 2 : 1;
      globalModifier.IncreaseUndeadAttackBonus(atkBuff, (Entity) minion);
    });
  }
}
