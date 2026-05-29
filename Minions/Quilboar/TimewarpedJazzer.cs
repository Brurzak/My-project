// Decompiled with JetBrains decompiler
// Type: BobsBuddy.Minions.Quilboar.TimewarpedJazzer
// Assembly: BobsBuddy, Version=1.33.32.0, Culture=neutral, PublicKeyToken=null
// MVID: 4041A954-F5FD-4AD8-89CE-27FF37FFCCA4
// Assembly location: C:\Users\franc\Desktop\BobsBuddy.dll

using BobsBuddy.Simulation;
using System;

#nullable enable
namespace BobsBuddy.Minions.Quilboar;

public class TimewarpedJazzer(string cardId, bool controlledByPlayer, Simulator simulator) : 
  Minion(cardId, controlledByPlayer, simulator),
  IDeathrattle,
  IEntity
{
  public const string CardId = "BG34_Giant_306";
  public const string Text = "<b>Deathrattle:</b> Your <b>Blood Gems</b> give an extra +{1} Health this game.";
  public const string GoldenText = "<b>Deathrattle:</b> Your <b>Blood Gems</b> give an extra +{1} Health this game.";

  public Action<Minion> GetDeathrattle() => TimewarpedJazzer.Deathrattle(this.golden);

  public static Action<Minion> Deathrattle(bool golden)
  {
    return (Action<Minion>) (minion =>
    {
      int num = golden ? 4 : 2;
      if (minion.ControlledByPlayer)
        minion.Simulator.state.Player.BloodGemHealthBuff += num;
      else
        minion.Simulator.state.Opponent.BloodGemHealthBuff += num;
    });
  }
}
