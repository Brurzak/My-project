// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Pirate.CruiseController
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Pirate;

public class CruiseController(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG31_821";
  public const string Text = "<b>Deathrattle:</b> For the rest of this combat, after you summon a Pirate, give it +5 Attack.";
  public const string GoldenText = "<b>Deathrattle:</b> For the rest of this combat, after you summon a Pirate, give it +10 Attack.";

  public Action<Minion>? GetDeathrattle() => CruiseController.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      globalModifier.PirateSummonAttackBonus += golden ? 10 : 5;
    });
  }
}
