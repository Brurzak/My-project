// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Undead.Plaguerunner
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Undead;

public class Plaguerunner(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_690";
  public const string Text = "<b>Deathrattle:</b> Your Undead have +{0} Attack this game, wherever they are. <i>(+{1} if this died outside combat!)</i>";
  public const string GoldenText = "<b>Deathrattle:</b> Your Undead have +{0} Attack this game, wherever they are. <i>(+{1} if this died outside combat!)</i>";

  public Action<Minion> GetDeathrattle() => Plaguerunner.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = golden ? 4 : 2;
      globalModifier.IncreaseUndeadAttackBonus(atkBuff, (Entity) minion);
    });
  }
}
