// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Beast.GoldrinnTheGreatWolf
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Beast;

public class GoldrinnTheGreatWolf(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BGS_018";
  public const string Text = "<b>Deathrattle:</b> For the rest of this combat, your Beasts have +{0}/+{1}.";
  public const string GoldenText = "<b>Deathrattle:</b> For the rest of this combat, your Beasts have +{0}/+{1}.";

  public Action<Minion> GetDeathrattle() => GoldrinnTheGreatWolf.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      GlobalModifier globalModifier = minion.ControlledByPlayer ? minion.Simulator.state.Player.GlobalModifier : minion.Simulator.state.Opponent.GlobalModifier;
      if (globalModifier == null)
        return;
      int atkBuff = golden ? 16 /*0x10*/ : 8;
      int healthBuff = golden ? 16 /*0x10*/ : 8;
      globalModifier.IncreaseBeastBonus(atkBuff, healthBuff, (Entity) minion);
    });
  }
}
